using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    public record class BinarySensor : IHassComponent
    {
        public string GetConfigTopic() => $"homeassistant/binary_sensor/{UniqueId}/config";

        [JsonIgnore]
        public DeviceClassDescription DeviceClassDescription { get; init; }
            = DeviceClassDescription.None; // need to remove required because Text.Json can't serialize required and JsonIgnore properties

        [JsonPropertyName("state_topic")]
        public required string StateTopic { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("unique_id")]
        public required string UniqueId { get; init; }

        [JsonPropertyName("device_class")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DeviceClass { get; init; } = null;

        [JsonPropertyName("payload_on")]
        public required string PayloadOn { get; init; }

        [JsonPropertyName("payload_off")]
        public required string PayloadOff { get; init; }

        [JsonPropertyName("payload_not_available")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PayloadNotAvailable { get; init; } = null;

        [JsonPropertyName("icon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Icon { get; init; } = null;

        [JsonPropertyName("value_template")]
        public required string ValueTemplate { get; init; }

        [JsonPropertyName("device")]
        public required Device Device { get; init; }
    }
}
