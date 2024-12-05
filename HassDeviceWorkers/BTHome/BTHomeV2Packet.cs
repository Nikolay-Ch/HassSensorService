using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BTHomePacketDecoder
{
    public delegate object Formatter(byte[] rawData, ref int index);

    public abstract class BTHObjectAttribute(int dataLength, double dataFactor) : Attribute
    {
        public int DataLength { get; protected set; } = dataLength;
        public double DataFactor { get; protected set; } = dataFactor;

        public virtual object GetData(byte[] rawData, ref int index)
        {
            var data = rawData[index..(index + DataLength)];
            index += DataLength;

            return data;
        }
    }

    public abstract class NumericBTHObjectAttribute(int dataLength, double dataFactor) : BTHObjectAttribute(dataLength, dataFactor)
    {
        public override object GetData(byte[] rawData, ref int index)
        {
            var val = InnerGetData((byte[])base.GetData(rawData, ref index));

            if (DataFactor != 1)
                return Convert.ToDecimal(val) * (decimal)DataFactor;

            return val;
        }

        protected abstract object InnerGetData(byte[] arr);
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjectUnsigned8() : NumericBTHObjectAttribute(1, 1)
    {
        protected override object InnerGetData(byte[] data) => data[0];
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjecInt8tAttribute() : NumericBTHObjectAttribute(1, 1)
    {
        protected override object InnerGetData(byte[] data) => (sbyte)data[0];
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjectUnsigned16() : NumericBTHObjectAttribute(2, 1)
    {
        protected override object InnerGetData(byte[] data)
        {
            var val = BitConverter.ToUInt16(data);
            return val;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjectInt16Attribute() : NumericBTHObjectAttribute(2, 1)
    {
        protected override object InnerGetData(byte[] data)
        {
            var val = BitConverter.ToInt16(data);
            return val;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjectRealFromSigned16(double dataFactor) : NumericBTHObjectAttribute(2, dataFactor)
    {
        protected override object InnerGetData(byte[] data)
        {
            var val = BitConverter.ToInt16(data);
            return val;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BTHObjectRealFromUnsigned16(double dataFactor) : NumericBTHObjectAttribute(2, dataFactor)
    {
        protected override object InnerGetData(byte[] data)
        {
            var val = BitConverter.ToUInt16(data);
            return val;
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
        [BTHObjectUnsigned8]
        PacketId = 0x00,

        [BTHObjectUnsigned8]
        Battery = 0x01,

        [BTHObjectRealFromSigned16(0.01)]
        Temperature = 0x02,

        [BTHObjectRealFromUnsigned16(0.01)]
        Humidity = 0x03,

        [BTHObjectRealFromUnsigned16(0.001)]
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
