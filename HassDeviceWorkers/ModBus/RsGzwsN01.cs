using HassDeviceWorkers.ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassDeviceWorkers
{
    public class RsGzwsN01
    {
        public RsGzwsN01(byte[] rawModBusData)
        {
            Temperature = BitConverter.ToUInt16(rawModBusData.SubArray(0x02, 2).Reverse().ToArray()) / (double)10;
            Humidity = BitConverter.ToUInt16(rawModBusData.SubArray(0x00, 2).Reverse().ToArray()) / (double)10;
            //LightIntensity = BitConverter.ToUInt16(rawModBusData.SubArray(0x04, 2).Reverse().ToArray()) / (double)100;
        }

        public double Temperature { get; }
        public double Humidity { get; }
        public double LightIntensity { get; }
    }
}
