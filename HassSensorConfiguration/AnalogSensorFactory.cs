namespace HassSensorConfiguration
{
    public class AnalogSensorDescription : BaseSensorDescription
    {
        public StateClass StateClass { get; set; } = StateClass.Measurement;
        public string UnitOfMeasures { get; set; } = null;
    }

    public class AnalogSensorFactory : IHassComponentFactory
    {
        public string HassStateTopicName { get; set; } = "HassAnalogSensor";

        public BaseSensorDescription CreateSensorDescription() => new AnalogSensorDescription();

        public IHassComponent CreateComponent(BaseSensorDescription sensorDescription) =>
            new AnalogSensor
            {
                DeviceClassDescription = sensorDescription.DeviceClassDescription,
                Device = sensorDescription.Device,
                DeviceClass = sensorDescription.DeviceClass ?? sensorDescription.DeviceClassDescription.DeviceClass,
                Icon = sensorDescription.SensorIcon,
                Name = sensorDescription.SensorName ?? $"{sensorDescription.Device.Name}-{sensorDescription.DeviceClassDescription.ValueName}",
                StateClass = ((AnalogSensorDescription)sensorDescription).StateClass,
                StateTopic = sensorDescription.StateTopic ?? $"+/+/{HassStateTopicName}/{sensorDescription.Device.Identifiers[0]}",
                UniqueId = sensorDescription.UniqueId ?? $"{sensorDescription.Device.Identifiers[0]}-{sensorDescription.Device.Name}-{sensorDescription.DeviceClassDescription.ValueName}",
                UnitOfMeasurement = ((AnalogSensorDescription)sensorDescription).UnitOfMeasures ?? sensorDescription.DeviceClassDescription.UnitOfMeasures,
                ValueTemplate = ((AnalogSensorDescription)sensorDescription).ValueTemplate ?? $"{{{{ value_json.{sensorDescription.DeviceClassDescription.ValueName} | is_defined }}}}"
            };
    }
}
