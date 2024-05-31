using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    public record class BinarySensor : HassComponent
    {
        [SetsRequiredMembers]
        public BinarySensor(BinarySensorDescription sensorDescription) : base(sensorDescription)
        {
            PayloadOn = sensorDescription.PayloadOn ?? "On";
            PayloadOff = sensorDescription.PayloadOff ?? "Off";
            PayloadNotAvailable = sensorDescription.PayloadNotAvailable;
        }

        [JsonIgnore]
        override public string StateSubTopic => "HassBinarySensor";

        [JsonPropertyName("payload_on")]
        public required string PayloadOn { get; init; }

        [JsonPropertyName("payload_off")]
        public required string PayloadOff { get; init; }

        [JsonPropertyName("payload_not_available")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PayloadNotAvailable { get; init; } = null;
    }
}
