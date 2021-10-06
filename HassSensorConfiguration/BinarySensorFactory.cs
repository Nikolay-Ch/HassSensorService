namespace HassSensorConfiguration
{
    public class BinarySensorFactory
    {
        public string TopicDeviceName { get; set; } = "HassBinarySensor";

        public BinarySensor CreateBinarySensor(
            DeviceClassDescription deviceClassDescription,
            Device device,
            string deviceClass = null,
            string sensorIcon = null,
            string sensorName = null,
            string stateTopic = null,
            string uniqueId = null,
            string payloadOn = null,
            string payloadOff = null,
            string payloadNotAvailable = null
        ) => new()
        {
            DeviceClassDescription = deviceClassDescription,
            Device = device,
            DeviceClass = deviceClass ?? deviceClassDescription.DeviceClass,
            Icon = sensorIcon,
            Name = sensorName ?? $"{device.Name}-{deviceClassDescription.ValueName}",
            StateTopic = stateTopic ?? $"+/+/{TopicDeviceName}/{device.Identifiers[0]}",
            UniqueId = uniqueId ?? $"{device.Identifiers[0]}-{device.Name}-{deviceClassDescription.ValueName}",
            PayloadOn = payloadOn ?? "On",
            PayloadOff = payloadOff ?? "Off",
            PayloadNotAvailable = payloadNotAvailable
        };
    }
}
