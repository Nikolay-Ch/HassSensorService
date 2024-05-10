using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BTHomePacketDecoder
{
    public delegate object Formatter(byte[] rawData, ref int index);

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjectAttribute : Attribute
    {
        public int DataLength { get; set; }
        public double DataFactor { get; set; }

        public object? GetData(byte[] rawData, ref int index)
        {
            var data = rawData[index..(index + DataLength)];
            index += DataLength;

            switch (DataLength)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    Array.Resize(ref data, 4);
                    var val = BitConverter.ToInt32(data);

                    if (DataFactor != 1)
                        return val * (decimal)DataFactor;

                    return val;
            }

            return null;
        }
    }

    public enum DataFormat
    {
        Int = 0,
        IntWithOneDigitPrecision = 1,
        IntWithTwoDigitPrecision = 2,
    }

    public enum BTHomeObjectId
    {
        [BTHObject(DataLength = 1, DataFactor = 1)]
        PacketId = 0x00,

        [BTHObject(DataLength = 1, DataFactor = 1)]
        Battery = 0x01,

        [BTHObject(DataLength = 2, DataFactor = 0.01)]
        Temperature = 0x02,

        [BTHObject(DataLength = 2, DataFactor = 0.01)]
        Humidity = 0x03,

        [BTHObject(DataLength = 2, DataFactor = 0.001)]
        Voltage = 0x0c
    }

    public readonly record struct BTHomeV2Packet
    {
        public int Version { get; }
        public List<(BTHomeObjectId objectId, object value)> Values { get; } = [];

        public object this[BTHomeObjectId index] => Values.First(e=>e.objectId == index).value;

        public BTHomeV2Packet(byte[] rawData)
        {
            Version = (rawData[0] & 0b11100000) >> 5;

            var index = 1;
            while (index < rawData.Length)
            {
                var valId = (BTHomeObjectId)rawData[index++];

                var attr = valId.GetType().GetField(valId.ToString())?.GetCustomAttribute<BTHObjectAttribute>();
                if (attr == null)
                    continue;

                var val = attr.GetData(rawData, ref index);
                if (val == null)
                    continue;

                Values.Add((valId, val));
            }
        }
    }
}
