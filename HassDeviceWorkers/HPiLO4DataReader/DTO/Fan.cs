namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct Fan
    {
        public string Label { get; init; }
        public string Zone { get; init; }
        public HealthStatus Status { get; init; }
        public int SpeedInPercent { get; init; }
    }
}
