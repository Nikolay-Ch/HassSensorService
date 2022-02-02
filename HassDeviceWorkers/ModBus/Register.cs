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

    public class Register
    {
        public byte DeviceId { get; set; }
        public byte FunctionCode { get; set; }
        public string Parameter { get; set; }
        public int Length { get; set; }
        public string Units { get; set; } = "";
        public RegisterFormat Format { get; set; }
        public int Address { get; set; }
        public byte[] RawValue { get; set; } = Array.Empty<byte>();
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

        public Register(byte deviceId, byte functionCode, string parameter, int length, string units, RegisterFormat format, int address)
        {
            DeviceId = deviceId;
            FunctionCode = functionCode;
            Parameter = parameter;
            Length = length;
            Units = units;
            Format = format;
            Address = address;
        }

        public override string ToString() => $"{Parameter}: {Value} {Units}";
    }
}
