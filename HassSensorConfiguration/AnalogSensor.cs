using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HassSensorConfiguration
{
    public record class AnalogSensor : HassComponent
    {
        [SetsRequiredMembers]
        public AnalogSensor(AnalogSensorDescription sensorDescription) : base(sensorDescription)
        {
            StateClass = sensorDescription.StateClass;
            UnitOfMeasurement = sensorDescription.UnitOfMeasures ?? sensorDescription.DeviceClassDescription.UnitOfMeasures;
            Options = sensorDescription.Options;
        }

        [JsonIgnore]
        override public string StateSubTopic => "HassAnalogSensor";

        [DefaultValue(StateClass.None)]
        [JsonPropertyName("state_class")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StateClass StateClass { get; init; }

        [JsonPropertyName("unit_of_measurement")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UnitOfMeasurement { get; init; } = null;

        [JsonPropertyName("options")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string>? Options { get; init; } = null;
    }
}
