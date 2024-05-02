namespace HassSensorConfiguration
{
    public record class BinarySensorDescription : BaseSensorDescription
    {
        public string? PayloadOn { get; set; } = null;
        public string? PayloadOff { get; set; } = null;
        public string? PayloadNotAvailable { get; set; } = null;
    }

    public class BinarySensorFactory : IHassComponentFactory
    {
        public string HassStateTopicName { get; set; } = "HassBinarySensor";

        public IHassComponent CreateComponent(BaseSensorDescription sensorDescription) =>
            new BinarySensor
            {
                DeviceClassDescription = sensorDescription.DeviceClassDescription,
                Device = sensorDescription.Device,
                DeviceClass = sensorDescription.DeviceClass ?? sensorDescription.DeviceClassDescription.DeviceClass,
                Icon = sensorDescription.SensorIcon,
                Name = sensorDescription.SensorName ?? $"{sensorDescription.Device.Name}-{sensorDescription.DeviceClassDescription.ValueName}",
                StateTopic = ((BinarySensorDescription)sensorDescription).StateTopic ?? $"+/+/{HassStateTopicName}/{sensorDescription.Device.Identifiers[0]}",
                UniqueId = ((BinarySensorDescription)sensorDescription).UniqueId ?? $"{sensorDescription.Device.Identifiers[0]}-{sensorDescription.Device.Name}-{sensorDescription.DeviceClassDescription.ValueName}",
                PayloadOn = ((BinarySensorDescription)sensorDescription).PayloadOn ?? "On",
                PayloadOff = ((BinarySensorDescription)sensorDescription).PayloadOff ?? "Off",
                PayloadNotAvailable = ((BinarySensorDescription)sensorDescription).PayloadNotAvailable,
                ValueTemplate = sensorDescription.ValueTemplate ?? $"{{{{ value_json.{sensorDescription.DeviceClassDescription.ValueName} | is_defined }}}}"
            };
    }
}
