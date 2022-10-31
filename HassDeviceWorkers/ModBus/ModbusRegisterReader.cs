using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceWorkers.ModBus
{
    public class ModbusRegisterReader
    {
        protected ILogger<ModbusRegisterReader> Logger { get; }
        protected ModbusGatewayConfiguration ModbusGatewayConfiguration { get; }

        public ModbusRegisterReader(ModbusGatewayConfiguration modbusGatewayConfiguration, ILogger<ModbusRegisterReader> logger)
        {
            ModbusGatewayConfiguration = modbusGatewayConfiguration;

            Logger = logger;
        }

        private static SemaphoreSlim SemaphoreSlim { get; } = new(1, 1);
        public async Task<int> ReadRegister(Register registerToRead)
        {
            try
            {
                Logger.LogTrace("ReadRegister: Method starts, waiting for semaphore... DeviceId={deviceId}",
                    registerToRead.DeviceId);

                await SemaphoreSlim.WaitAsync();

                Logger.LogTrace("ReadRegister: Semaphore opened... DeviceId={deviceId}",
                    registerToRead.DeviceId);

                using var serialPort = CreateSerialPort();

                Logger.LogTrace("ReadRegister: Wait to quite on the Modbus... DeviceId={deviceId}",
                    registerToRead.DeviceId);
                
                // wait to quite on the Modbus
                Thread.Sleep(200);

                var bufWrite = new byte[8];
                bufWrite[0] = registerToRead.DeviceId;
                bufWrite[1] = registerToRead.FunctionCode;
                bufWrite[2] = HiByte(registerToRead.Address);
                bufWrite[3] = LoByte(registerToRead.Address);
                bufWrite[4] = HiByte(registerToRead.Length);
                bufWrite[5] = LoByte(registerToRead.Length);

                var crc16 = Crc.GetModbusCrc16(bufWrite.Take(6).ToArray());

                bufWrite[6] = crc16[0];
                bufWrite[7] = crc16[1];

                try
                {
                    serialPort.Write(bufWrite, 0, bufWrite.Length);
                    Logger.LogTrace("ReadRegister: Send data to Modbus: DeviceId={DeviceId}, {buf}",
                        registerToRead.DeviceId, Convert.ToHexString(bufWrite));

                    Logger.LogTrace("ReadRegister: Wait to request forming on the Modbus... DeviceId={deviceId}",
                        registerToRead.DeviceId);
                    // wait to request forming on the Modbus
                    Thread.Sleep(200);

                    // each register = 2 bytes
                    var bytesToRead = registerToRead.Length * 2;

                    // address + code + count + bytesToRead + errorCheck (2 bytes)
                    var bufRead = new byte[bytesToRead + 5];

                    registerToRead.RawValue = Array.Empty<byte>();

                    serialPort.Read(bufRead, 0, bufRead.Length);
                    Logger.LogTrace("ReadRegister: Receive data from Modbus: DeviceId={DeviceId}, {buf}",
                        registerToRead.DeviceId, Convert.ToHexString(bufRead));

                    if (bufRead[0] == registerToRead.DeviceId &&
                            bufRead[1] == registerToRead.FunctionCode &&
                            bufRead[2] == bytesToRead)
                        registerToRead.RawValue = bufRead.Skip(3).Take(bytesToRead).ToArray();
                    else
                        Logger.LogInformation("ReadRegister: Error receiving data for DeviceId!" +
                            " Expected: {DeviceId}, {FunctionCode}, {ButesToRead}," +
                            " but receive: {DeviceIdReveived}, {FunctionCodeReceived}, {ButesReceived}",
                            registerToRead.DeviceId, registerToRead.FunctionCode, bytesToRead,
                            bufRead[0], bufRead[1], bufRead[2]);
                }
                catch (IOException ex)
                {
                    Logger.LogError(ex, "{MethodName} I/O error '{Message}' at {DateTime}",
                        new StackTrace(ex).GetFrame(0).GetMethod().Name, ex.Message, DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "{MethodName} error '{Message}' at {DateTime}",
                        new StackTrace(ex).GetFrame(0).GetMethod().Name, ex.Message, DateTimeOffset.Now);
                }

                Logger.LogTrace("ReadRegister: Method ends... DeviceId={deviceId}",
                    registerToRead.DeviceId);

                return registerToRead.RawValue.Length;
            }
            finally
            {
                SemaphoreSlim.Release();

                Logger.LogTrace("ReadRegister: Free semaphore... DeviceId={deviceId}",
                    registerToRead.DeviceId);
            }
        }

        private static byte LoByte(int addressToRead) => (byte)(addressToRead & 0x00ff);

        private static byte HiByte(int addressToRead) => (byte)(addressToRead >> 8);

        private ISerialPort CreateSerialPort() =>
            new RemoteSerialPort(
                ModbusGatewayConfiguration.GatewayAddress,
                ModbusGatewayConfiguration.GatewayPort) { ReadTimeout = 3000 };
    }
}
