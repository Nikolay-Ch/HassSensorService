using System;
using System.Linq;

namespace HassDeviceWorkers.ModBus
{
    public enum RegisterFormat
    {
        Undefined = 0,
        Float = 1,
        Hex = 2,
        UnsignedInt = 3,
        OneToThousand = 4
    }

    public class Register(byte deviceId, byte functionCode, string parameter, int length, string units, RegisterFormat format, int address)
    {
        public byte DeviceId { get; set; } = deviceId;
        public byte FunctionCode { get; set; } = functionCode;
        public string Parameter { get; set; } = parameter;
        public int Length { get; set; } = length;
        public string Units { get; set; } = units;
        public RegisterFormat Format { get; set; } = format;
        public int Address { get; set; } = address;
        public byte[] RawValue { get; set; } = [];
        public object Value
        {
            get
            {
                if (RawValue.Length == 0)
                    return "";

                return Format switch
                {
                    RegisterFormat.Float => BitConverter.ToSingle(RawValue.Reverse().ToArray()),
                    RegisterFormat.UnsignedInt => BitConverter.ToUInt32(RawValue.Reverse().ToArray()),
                    RegisterFormat.Hex => BitConverter.ToString(RawValue),
                    RegisterFormat.OneToThousand => BitConverter.ToUInt32(RawValue.Reverse().ToArray()) / (double)1000,
                    _ => "",
                };
            }
        }

        public override string ToString() => $"{Parameter}: {Value} {Units}";
    }
}
