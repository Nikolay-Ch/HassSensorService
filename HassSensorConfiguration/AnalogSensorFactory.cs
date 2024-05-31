using System.Collections.Generic;

namespace HassSensorConfiguration
{
    public record class AnalogSensorDescription : BaseSensorDescription
    {
        public StateClass StateClass { get; set; } = StateClass.Measurement;
        public string? UnitOfMeasures { get; set; } = null;
        public IEnumerable<string>? Options { get; set; } = null;
    }

    public class AnalogSensorFactory : IHassComponentFactory
    {
        public IHassComponent CreateComponent(BaseSensorDescription sensorDescription) =>
            new AnalogSensor((AnalogSensorDescription)sensorDescription);
    }
}
