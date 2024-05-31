namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct MemorySlot
    {
        public readonly string? Name { get; init; }
        public readonly string? Size { get; init; }
        public readonly string? Speed {  get; init; }
    }
}
