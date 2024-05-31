namespace HassSensorConfiguration
{
    public record class BaseSensorDescription
    {
        public required DeviceClassDescription DeviceClassDescription { get; set; }
        public required Device Device { get; set; }
        public string? SensorIcon { get; set; } = null;
        public string? SensorName { get; set; } = null;
        public string? UniqueId { get; set; } = null;
        public bool HasAttributes { get; set; } = false;
        public string? EntityCategory { get; set; } = null;
    }
}
