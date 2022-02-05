using Newtonsoft.Json;
using System.ComponentModel;

namespace HassSensorConfiguration
{
    public partial class AnalogSensor : IHassComponent
    {
        public string GetConfigTopic() => $"homeassistant/sensor/{UniqueId}/config";

        [JsonIgnore]
        public DeviceClassDescription DeviceClassDescription { get; init; }

        [DefaultValue(StateClass.None)]
        [JsonProperty("state_class", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public StateClass StateClass { get; init; }

        [JsonProperty("state_topic")]
        public string StateTopic { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("unique_id")]
        public string UniqueId { get; init; }

        [JsonProperty("device_class", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceClass { get; init; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; init; }

        [JsonProperty("value_template")]
        public string ValueTemplate { get; init; }

        [JsonProperty("unit_of_measurement", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitOfMeasurement { get; init; }

        [JsonProperty("device")]
        public Device Device { get; init; }
    }
}
