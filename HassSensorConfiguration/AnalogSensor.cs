using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    public record class AnalogSensor : IHassComponent
    {
        public string GetConfigTopic() => $"homeassistant/sensor/{UniqueId}/config";

        [JsonIgnore]
        public DeviceClassDescription DeviceClassDescription { get; init; }
            = DeviceClassDescription.None; // need to remove required because Text.Json can't serialize required and JsonIgnore properties

        [DefaultValue(StateClass.None)]
        [JsonPropertyName("state_class")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StateClass StateClass { get; init; }

        [JsonPropertyName("state_topic")]
        public required string StateTopic { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("unique_id")]
        public required string UniqueId { get; init; }

        [JsonPropertyName("device_class")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DeviceClass { get; init; } = null;

        [JsonPropertyName("icon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Icon { get; init; } = null;

        [JsonPropertyName("value_template")]
        public required string ValueTemplate { get; init; }

        [JsonPropertyName("unit_of_measurement")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UnitOfMeasurement { get; init; } = null;

        [JsonPropertyName("device")]
        public required Device Device { get; init; }
    }
}
