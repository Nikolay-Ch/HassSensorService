using System;
using System.Linq;

namespace HassDeviceWorkers.ModBus
{
    public record class Sdm120(byte[] RawModBusData)
    {
        public float Voltage { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x00, 4).Reverse().ToArray());
        public float Current { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x0c, 4).Reverse().ToArray());
        public float ActivePower { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x18, 4).Reverse().ToArray());
        public float ApparentPower { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x24, 4).Reverse().ToArray());
        public float ReactivePower { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x30, 4).Reverse().ToArray());
        public float PowerFactor { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x3c, 4).Reverse().ToArray());
        public float Frequency { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x8c, 4).Reverse().ToArray());
        public float ImportActiveEnergy { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x90, 4).Reverse().ToArray());
        public float ExportActiveEnergy { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x94, 4).Reverse().ToArray());
        public float ImportReactiveEnergy { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x98, 4).Reverse().ToArray());
        public float ExportReactiveEnergy { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x9c, 4).Reverse().ToArray());
        public float TotalSystemPowerDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0xa8, 4).Reverse().ToArray());
        public float MaximumTotalSystemPowerDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0xac, 4).Reverse().ToArray());
        public float ImportSystemPowerDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0xb0, 4).Reverse().ToArray());
        public float MaximumImportSystemPowerDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0xb4, 4).Reverse().ToArray());
        public float ExportSystemPowerDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0xb8, 4).Reverse().ToArray());
        public float MaximumExportSystemPowerDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0xbc, 4).Reverse().ToArray());
        public float CurrentDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x204, 4).Reverse().ToArray());
        public float MaximumCurrentDemand { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x210, 4).Reverse().ToArray());
        public float TotalActiveEnergy { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x2ac, 4).Reverse().ToArray());
        public float TotalReactiveEnergy { get; } = BitConverter.ToSingle(RawModBusData.SubArray(0x2b0, 4).Reverse().ToArray());
    }
}
