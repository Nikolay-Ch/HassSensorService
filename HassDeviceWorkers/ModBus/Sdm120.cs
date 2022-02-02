using System;
using System.Linq;

namespace HassDeviceWorkers.ModBus
{
    public class Sdm120
    {
        public Sdm120(byte[] rawModBusData)
        {
            Voltage = BitConverter.ToSingle(rawModBusData.SubArray(0x00, 4).Reverse().ToArray());
            Current = BitConverter.ToSingle(rawModBusData.SubArray(0x0c, 4).Reverse().ToArray());
            ActivePower = BitConverter.ToSingle(rawModBusData.SubArray(0x18, 4).Reverse().ToArray());
            ApparentPower = BitConverter.ToSingle(rawModBusData.SubArray(0x24, 4).Reverse().ToArray());
            ReactivePower = BitConverter.ToSingle(rawModBusData.SubArray(0x30, 4).Reverse().ToArray());
            PowerFactor = BitConverter.ToSingle(rawModBusData.SubArray(0x3c, 4).Reverse().ToArray());
            Frequency = BitConverter.ToSingle(rawModBusData.SubArray(0x8c, 4).Reverse().ToArray());
        }

        public float Voltage { get; }
        public float Current { get; }
        public float ActivePower { get; }
        public float ApparentPower { get; }
        public float ReactivePower { get; }
        public float PowerFactor { get; }
        public float Frequency { get; }
    }
}
