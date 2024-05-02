using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StateClass
    {
        None = 0,

        [EnumMember(Value = "measurement")]
        Measurement = 1,

        [EnumMember(Value = "total_increasing")]
        TotalIncreasing = 2
    }
}
