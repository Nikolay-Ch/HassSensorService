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
            PhaseVoltage1 = BitConverter.ToUInt32(rawModBusData.SubArray(0x00, 4).Reverse().ToArray()) / (double)1000;
            PhaseVoltage2 = BitConverter.ToUInt32(rawModBusData.SubArray(0x04, 4).Reverse().ToArray()) / (double)1000;
            PhaseVoltage3 = BitConverter.ToUInt32(rawModBusData.SubArray(0x08, 4).Reverse().ToArray()) / (double)1000;
            LineVoltage1 = BitConverter.ToUInt32(rawModBusData.SubArray(0x0c, 4).Reverse().ToArray()) / (double)1000;
            LineVoltage2 = BitConverter.ToUInt32(rawModBusData.SubArray(0x10, 4).Reverse().ToArray()) / (double)1000;
            LineVoltage3 = BitConverter.ToUInt32(rawModBusData.SubArray(0x14, 4).Reverse().ToArray()) / (double)1000;
            Frequency = BitConverter.ToUInt32(rawModBusData.SubArray(0x64, 4).Reverse().ToArray()) / (double)1000;
            VoltageUnbalance = BitConverter.ToUInt32(rawModBusData.SubArray(0x68, 4).Reverse().ToArray()) / (double)1000;
        }

        public double PhaseVoltage1 { get; }
        public double PhaseVoltage2 { get; }
        public double PhaseVoltage3 { get; }
        public double LineVoltage1 { get; }
        public double LineVoltage2 { get; }
        public double LineVoltage3 { get; }
        public double Frequency { get; }
        public double VoltageUnbalance { get; }
    }
}
