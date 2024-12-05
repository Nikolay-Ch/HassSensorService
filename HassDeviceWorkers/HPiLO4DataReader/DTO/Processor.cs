namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct Processor
    {
        public readonly string? Name { get; init; }
        public readonly string? Speed { get; init; }
        public readonly string? ExecutionTechnology { get; init; }
        public readonly string? MemoryTechnology { get; init; }
    }
}
