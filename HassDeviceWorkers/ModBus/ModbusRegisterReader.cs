using System;
using System.IO;
using System.Linq;
using System.Threading;
using Tako.CRC;

namespace HassDeviceWorkers.ModBus
{
    public class ModbusRegisterReader : IDisposable
    {
        public ISerialPort SerialPort { get; set; }

        public int ReadRegister(Register registerToRead)
        {
            var bufwrite = new byte[8];
            bufwrite[0] = registerToRead.DeviceId;
            bufwrite[1] = registerToRead.FunctionCode;
            bufwrite[2] = HiByte(registerToRead.Address);
            bufwrite[3] = LoByte(registerToRead.Address);
            bufwrite[4] = HiByte(registerToRead.Length);
            bufwrite[5] = LoByte(registerToRead.Length);

            var crc16 = new CRCManager()
                .CreateCRCProvider(EnumCRCProvider.CRC16Modbus)
                .GetCRC(bufwrite.Take(6).ToArray())
                .CrcArray;

            bufwrite[6] = crc16[1];
            bufwrite[7] = crc16[0];

            SerialPort.Write(bufwrite, 0, bufwrite.Length);

            Thread.Sleep(200);

            // each register = 2 bytes
            var bytesToRead = registerToRead.Length * 2;

            // address + code + count + bytesToRead + errorCheck (2 bytes)
            var bufRead = new byte[bytesToRead + 5];

            try
            {
                SerialPort.Read(bufRead, 0, bufRead.Length);
                registerToRead.RawValue = bufRead.Skip(3).Take(bytesToRead).ToArray();
            }
            catch (IOException)
            {
                registerToRead.RawValue = Array.Empty<byte>();
            }

            return registerToRead.RawValue.Length;
        }

        private static byte LoByte(int addressToRead) => (byte)(addressToRead & 0x00ff);

        private static byte HiByte(int addressToRead) => (byte)(addressToRead >> 8);

        public void Dispose() => SerialPort.Dispose();
    }
}
