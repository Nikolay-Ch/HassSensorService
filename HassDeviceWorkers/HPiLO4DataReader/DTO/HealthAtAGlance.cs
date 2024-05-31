namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct HealthAtAGlance
    {
        public HealthStatus BiosHardware { get; init; }
        public HealthStatus Fans { get; init; }
        public HealthStatus Temperature { get; init; }
        public HealthStatus PowerSupplies { get; init; }
        public HealthStatus Processor { get; init; }
        public HealthStatus Memory { get; init; }
        public HealthStatus Network { get; init; }
        public HealthStatus Storage { get; init; }
    }
}
