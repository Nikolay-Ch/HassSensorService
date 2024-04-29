using HassDeviceWorkers.ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassDeviceWorkers
{
    public class RsGzwsN01(byte[] rawModBusData)
    {
        public double Temperature { get; } = BitConverter.ToInt16(rawModBusData.SubArray(0x02, 2).Reverse().ToArray()) / (double)10;
        public double Humidity { get; } = BitConverter.ToUInt16(rawModBusData.SubArray(0x00, 2).Reverse().ToArray()) / (double)10;
        public uint LightIntensity { get; } = BitConverter.ToUInt32(rawModBusData.SubArray(0x04, 4).Reverse().ToArray());
    }
}
