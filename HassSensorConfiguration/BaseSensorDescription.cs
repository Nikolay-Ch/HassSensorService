namespace HassSensorConfiguration
{
    public record class BaseSensorDescription
    {
        public required DeviceClassDescription DeviceClassDescription { get; set; }
        public required Device Device { get; set; }
        public string? DeviceClass { get; set; } = null;
        public string? SensorIcon { get; set; } = null;
        public string? SensorName { get; set; } = null;
        public string? StateTopic { get; set; } = null;
        public string? UniqueId { get; set; } = null;
        public string? ValueTemplate { get; set; } = null;
    }
}
