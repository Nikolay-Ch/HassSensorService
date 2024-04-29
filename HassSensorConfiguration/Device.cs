using Newtonsoft.Json;
using System.Collections.Generic;

namespace HassSensorConfiguration
{
    public class Device
    {
        [JsonProperty("connections")]
        public List<List<string>> Connections { get; init; } = [];

        [JsonProperty("identifiers")]
        public List<string> Identifiers { get; init; } = [];

        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public string Manufacturer { get; init; }

        [JsonProperty("model")]
        public string Model { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("sw_version", NullValueHandling = NullValueHandling.Ignore)]
        public string SoftwareVersion { get; init; }

        [JsonProperty("via_device", NullValueHandling = NullValueHandling.Ignore)]
        public string ViaDevice { get; init; }
    }
}
