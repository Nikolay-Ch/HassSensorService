namespace HassSensorConfiguration
{
    public interface IHassComponentFactory
    {
        //BaseSensorDescription CreateSensorDescription();
        IHassComponent CreateComponent(BaseSensorDescription sensorDescription);
    }
}
