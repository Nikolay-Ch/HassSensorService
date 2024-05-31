namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct PowerSupply
    {
        public string Label { get; init; }
        public string Status { get; init; }
    }
}
