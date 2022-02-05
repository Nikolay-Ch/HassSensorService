using Newtonsoft.Json;

namespace HassSensorConfiguration
{
    public partial class BinarySensor : IHassComponent
    {
        public string GetConfigTopic() => $"homeassistant/binary_sensor/{UniqueId}/config";

        [JsonIgnore]
        public DeviceClassDescription DeviceClassDescription { get; init; }

        [JsonProperty("state_topic")]
        public string StateTopic { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("unique_id")]
        public string UniqueId { get; init; }

        [JsonProperty("device_class", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceClass { get; init; }

        [JsonProperty("payload_on")]
        public string PayloadOn { get; init; }

        [JsonProperty("payload_off")]
        public string PayloadOff { get; init; }

        [JsonProperty("payload_not_available", NullValueHandling = NullValueHandling.Ignore)]
        public string PayloadNotAvailable { get; init; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; init; }

        [JsonProperty("value_template")]
        public string ValueTemplate { get; init; }

        [JsonProperty("device")]
        public Device Device { get; init; }
    }
}
