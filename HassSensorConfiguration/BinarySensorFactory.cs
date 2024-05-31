namespace HassSensorConfiguration
{
    public record class BinarySensorDescription : BaseSensorDescription
    {
        public string? PayloadOn { get; set; } = null;
        public string? PayloadOff { get; set; } = null;
        public string? PayloadNotAvailable { get; set; } = null;
    }

    public class BinarySensorFactory : IHassComponentFactory
    {
        public IHassComponent CreateComponent(BaseSensorDescription sensorDescription) =>
            new BinarySensor((BinarySensorDescription)sensorDescription);
    }
}
