namespace HassSensorConfiguration
{
    public interface IHassComponent
    {
        Device Device { get; init; }
        string? DeviceClass { get; init; }
        DeviceClassDescription DeviceClassDescription { get; init; }
        string? Icon { get; init; }
        string Name { get; init; }
        string StateTopic { get; init; }
        string UniqueId { get; init; }

        string GetConfigTopic();
    }
}
