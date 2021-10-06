namespace HassSensorConfiguration
{
    public class SensorFactory
    {
        public string HomeAssistatnStateTopicName { get; set; } = "BTtoMQTT";

        public Sensor CreateSensor(
            DeviceClassDescription deviceClassDescription,
            Device device,
            string deviceClass = null,
            string sensorIcon = null,
            string sensorName = null,
            StateClass stateClass = StateClass.None,
            string stateTopic = null,
            string uniqueId = null,
            string unitOfMeasures = null,
            string valueTemplate = null
        ) => new()
        {
            DeviceClassDescription = deviceClassDescription,
            Device = device,
            DeviceClass = deviceClass ?? deviceClassDescription.DeviceClass,
            Icon = sensorIcon,
            Name = sensorName ?? $"{device.Name}-{deviceClassDescription.ValueName}",
            StateClass = stateClass,
            StateTopic = stateTopic ?? $"+/+/{HomeAssistatnStateTopicName}/{device.Identifiers[0]}",
            UniqueId = uniqueId ?? $"{device.Identifiers[0]}-{device.Name}-{deviceClassDescription.ValueName}",
            UnitOfMeasurement = unitOfMeasures ?? deviceClassDescription.UnitOfMeasures,
            ValueTemplate =  valueTemplate ?? $"{{{{ value_json.{deviceClassDescription.ValueName} | is_defined }}}}"
        };
    }
}
