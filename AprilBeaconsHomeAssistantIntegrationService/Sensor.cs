namespace AprilBeaconsHomeAssistantIntegrationService
{
    public enum SensorCategory
    {
        sensor,
        binary_sensor
    }

    public class Sensor
    {
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public string Class { get; set; }
        public SensorCategory Category { get; set; }
        public string SensorIcon { get; set; }
        public string ValueName { get; set; }
        public string Unit { get; set; }
    }
}
