using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceWorkers.ModBus
{
    public class ModbusRegisterReader : IDisposable
    {
        protected ILogger<ModbusRegisterReader> Logger { get; }
        protected ISerialPort SerialPort { get; }

        public ModbusRegisterReader(ModbusGatewayConfiguration modbusConfiguration, ILogger<ModbusRegisterReader> logger)
        {
            SerialPort = CreateSerialPort(modbusConfiguration.GatewayAddress, modbusConfiguration.GatewayPort);

            Logger = logger;
        }

        private static SemaphoreSlim SemaphoreSlim { get; } = new(1, 1);

        public async Task<int> ReadRegister(Register registerToRead)
        {
            await SemaphoreSlim.WaitAsync();

            try
            {
                var bufwrite = new byte[8];
                bufwrite[0] = registerToRead.DeviceId;
                bufwrite[1] = registerToRead.FunctionCode;
                bufwrite[2] = HiByte(registerToRead.Address);
                bufwrite[3] = LoByte(registerToRead.Address);
                bufwrite[4] = HiByte(registerToRead.Length);
                bufwrite[5] = LoByte(registerToRead.Length);

                var crc16 = Crc.GetModbusCrc16(bufwrite.Take(6).ToArray());

                bufwrite[6] = crc16[0];
                bufwrite[7] = crc16[1];

                SerialPort.Write(bufwrite, 0, bufwrite.Length);

                Thread.Sleep(200);

                // each register = 2 bytes
                var bytesToRead = registerToRead.Length * 2;

                // address + code + count + bytesToRead + errorCheck (2 bytes)
                var bufRead = new byte[bytesToRead + 5];

                registerToRead.RawValue = Array.Empty<byte>();

                try
                {
                    SerialPort.Read(bufRead, 0, bufRead.Length);

                    if (bufRead[0] == registerToRead.DeviceId &&
                            bufRead[1] == registerToRead.FunctionCode &&
                            bufRead[2] == bytesToRead)
                        registerToRead.RawValue = bufRead.Skip(3).Take(bytesToRead).ToArray();
                    else
                        Logger.LogInformation("Error receiving data for DeviceId!" +
                            " Expected: {DeviceId}, {FunctionCode}, {ButesToRead}," +
                            " but receive: {DeviceIdReveived}, {FunctionCodeReceived}, {ButesReceived}",
                            registerToRead.DeviceId, registerToRead.FunctionCode, bytesToRead,
                            bufRead[0], bufRead[1], bufRead[2]);
                }
                catch (IOException) { }
            }
            finally
            {
                SemaphoreSlim.Release();
            }

            return registerToRead.RawValue.Length;
        }

        private static byte LoByte(int addressToRead) => (byte)(addressToRead & 0x00ff);

        private static byte HiByte(int addressToRead) => (byte)(addressToRead >> 8);

        private ISerialPort CreateSerialPort(string gatewayAddress, int gatewayPort) =>
            new RemoteSerialPort(gatewayAddress, gatewayPort) { ReadTimeout = 3000 };


        #region implementation of IDisposable with Dispose pattern
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    SerialPort.Dispose();

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ModbusRegisterReader()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
