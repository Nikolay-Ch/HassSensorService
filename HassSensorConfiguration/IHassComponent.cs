using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    public interface IHassComponent
    {
        Device Device { get; init; }
        string? DeviceClass { get; init; }
        DeviceClassDescription DeviceClassDescription { get; init; }
        string? Icon { get; init; }
        string Name { get; init; }
        string StateTopic { get; }
        string? AttributesTopic { get; init; }
        string? AttributesTemplate { get; init; }
        string UniqueId { get; init; }

        string StateSubTopic { get; }
    }

    public abstract record class HassComponent : IHassComponent
    {
        [SetsRequiredMembers]
        public HassComponent(BaseSensorDescription sensorDescription)
        {
            DeviceClassDescription = sensorDescription.DeviceClassDescription;
            Device = sensorDescription.Device;
            DeviceClass = sensorDescription.DeviceClassDescription.DeviceClass;
            Icon = sensorDescription.SensorIcon;
            Name = sensorDescription.SensorName ?? $"{sensorDescription.Device.Model}-{sensorDescription.DeviceClassDescription.ValueName}";
            UniqueId = sensorDescription.UniqueId ?? $"{sensorDescription.Device.Identifiers[0]}-{sensorDescription.DeviceClassDescription.ValueName}";
            ValueTemplate = $"{{{{ value_json.{sensorDescription.DeviceClassDescription.ValueName} | is_defined }}}}";
            EntityCategory = sensorDescription.EntityCategory;

            if (sensorDescription.HasAttributes)
            {
                AttributesTopic = $"{StateTopic}/{UniqueId}-attributes";
                //AttributesTemplate = $"{{{{ value_json.data.value | tojson }}}}";
            }
        }

        [JsonIgnore]
        abstract public string StateSubTopic { get; }

        [JsonIgnore]
        public DeviceClassDescription DeviceClassDescription { get; init; }
            = DeviceClassDescription.None; // need to remove required because Text.Json can't serialize required and JsonIgnore properties

        [JsonPropertyName("state_topic")]
        virtual public string StateTopic => $"+/+/{StateSubTopic}/{Device.Identifiers[0]}";

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

        [JsonPropertyName("json_attributes_topic")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AttributesTopic { get; init; } = null;

        [JsonPropertyName("json_attributes_template")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AttributesTemplate { get; init; } = null;

        [JsonPropertyName("entity_category")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EntityCategory { get; init; } = null;

        [JsonPropertyName("device")]
        public required Device Device { get; init; }
    }
}
