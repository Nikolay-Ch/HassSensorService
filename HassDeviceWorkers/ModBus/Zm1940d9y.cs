using System;
using System.Linq;

namespace HassDeviceWorkers.ModBus
{
    /// <summary>
    /// Parse raw data (max 63 values)
    /// </summary>
    /// <param name="rawModBusData"></param>
    public class Zm1940d9y(byte[] rawModBusData)
    {
        public double Phase1Voltage { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x00, 4).Reverse().ToArray()) / (double)1000;
        public double Phase2Voltage { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x04, 4).Reverse().ToArray()) / (double)1000;
        public double Phase3Voltage { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x08, 4).Reverse().ToArray()) / (double)1000;
        public double Line1To2Voltage { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x0c, 4).Reverse().ToArray()) / (double)1000;
        public double Line2To3Voltage { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x10, 4).Reverse().ToArray()) / (double)1000;
        public double Line3To1Voltage { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x14, 4).Reverse().ToArray()) / (double)1000;
        public double Frequency { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x64, 4).Reverse().ToArray()) / (double)1000;
        public double VoltageUnbalance { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x68, 4).Reverse().ToArray()) / (double)10;
    }
}
