using System;
using System.Linq;

namespace HassDeviceWorkers.ModBus
{
    public class Zm1940d9y
    {
        /// <summary>
        /// Parse raw data (max 63 values)
        /// </summary>
        /// <param name="rawModBusData"></param>
        public Zm1940d9y(byte[] rawModBusData)
        {
            Phase1Voltage = BitConverter.ToUInt32(rawModBusData.SubArray(0x00, 4).Reverse().ToArray()) / (double)1000;
            Phase2Voltage = BitConverter.ToUInt32(rawModBusData.SubArray(0x04, 4).Reverse().ToArray()) / (double)1000;
            Phase3Voltage = BitConverter.ToUInt32(rawModBusData.SubArray(0x08, 4).Reverse().ToArray()) / (double)1000;
            Line1To2Voltage = BitConverter.ToUInt32(rawModBusData.SubArray(0x0c, 4).Reverse().ToArray()) / (double)1000;
            Line2To3Voltage = BitConverter.ToUInt32(rawModBusData.SubArray(0x10, 4).Reverse().ToArray()) / (double)1000;
            Line3To1Voltage = BitConverter.ToUInt32(rawModBusData.SubArray(0x14, 4).Reverse().ToArray()) / (double)1000;
            Frequency = BitConverter.ToUInt32(rawModBusData.SubArray(0x64, 4).Reverse().ToArray()) / (double)1000;
            VoltageUnbalance = BitConverter.ToUInt32(rawModBusData.SubArray(0x68, 4).Reverse().ToArray()) / (double)10;
        }

        public double Phase1Voltage { get; }
        public double Phase2Voltage { get; }
        public double Phase3Voltage { get; }
        public double Line1To2Voltage { get; }
        public double Line2To3Voltage { get; }
        public double Line3To1Voltage { get; }
        public double Frequency { get; }
        public double VoltageUnbalance { get; }
    }
}
