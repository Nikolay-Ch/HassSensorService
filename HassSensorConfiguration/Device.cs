using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    public record class Device
    {
        [JsonPropertyName("connections")]
        public List<List<string>> Connections { get; init; } = [];

        [JsonPropertyName("identifiers")]
        public List<string> Identifiers { get; init; } = [];

        [JsonPropertyName("manufacturer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Manufacturer { get; init; } = "";

        [JsonPropertyName("model")]
        public required string Model { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("serial_number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("sw_version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SoftwareVersion { get; init; }

        [JsonPropertyName("via_device")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required string ViaDevice { get; init; }
    }
}
