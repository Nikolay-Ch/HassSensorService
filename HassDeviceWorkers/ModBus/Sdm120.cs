using System;
using System.Linq;

namespace HassDeviceWorkers.ModBus
{
    public class Sdm120(byte[] rawModBusData)
    {
        public float Voltage { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x00, 4).Reverse().ToArray());
        public float Current { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x0c, 4).Reverse().ToArray());
        public float ActivePower { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x18, 4).Reverse().ToArray());
        public float ApparentPower { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x24, 4).Reverse().ToArray());
        public float ReactivePower { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x30, 4).Reverse().ToArray());
        public float PowerFactor { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x3c, 4).Reverse().ToArray());
        public float Frequency { get; } = BitConverter.ToSingle(rawModBusData.SubArray(0x8c, 4).Reverse().ToArray());
    }
}
