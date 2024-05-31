namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct Temperature
    {
        public string Label { get; init; }
        public LocationArea Location { get; init; }
        public HealthStatus Status { get; init; }
        public int? CurrentTemperature { get; init; }
        public int? CautionTemperature { get; init; }
        public int? CriticalTemperature { get; init; }
    }
}
