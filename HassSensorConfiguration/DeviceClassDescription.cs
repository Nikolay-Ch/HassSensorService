namespace HassSensorConfiguration
{
    public enum SensorClass
    {
        Temperature,
        Humidity,
        Illuminance,
        Battery,
        Moisture,
        Fertility
    }

    public class DeviceClassDescription
    {
        public string DeviceClass { get; init; } // name of device class. This property has null-value for non-standard device-classes
        public string ValueName { get; init; } // name of property, contained value in value-MQTT message
        public string UnitOfMeasures { get; init; }

        public DeviceClassDescription() { }

        public DeviceClassDescription(DeviceClassDescription deviceClassDescription)
        {
            DeviceClass = deviceClassDescription.DeviceClass;
            ValueName = deviceClassDescription.ValueName;
            UnitOfMeasures = deviceClassDescription.UnitOfMeasures;
        }

        public static DeviceClassDescription None => new();

        public static DeviceClassDescription Temperature => new()
        { DeviceClass = "temperature", ValueName = "temp", UnitOfMeasures = "°C" };

        public static DeviceClassDescription Humidity => new()
        { DeviceClass = "humidity", ValueName = "hum", UnitOfMeasures = "%" };

        public static DeviceClassDescription IlluminanceLux => new()
        { DeviceClass = "illuminance", ValueName = "lux", UnitOfMeasures = "lx" };

        public static DeviceClassDescription IlluminanceLuminous => new()
        { DeviceClass = "illuminance", ValueName = "lum", UnitOfMeasures = "lm" };

        public static DeviceClassDescription Battery => new()
        { DeviceClass = "battery", ValueName = "batt", UnitOfMeasures = "%" };

        public static DeviceClassDescription Moisture => new()
        { ValueName = "moi", UnitOfMeasures = "%" };

        public static DeviceClassDescription Fertility => new()
        { ValueName = "fer", UnitOfMeasures = "µS/cm" };

        public static DeviceClassDescription Gas => new()
        { DeviceClass = "gas", ValueName = "gas", UnitOfMeasures = "m³" };

        public static DeviceClassDescription EnergyKw => new()
        { DeviceClass = "energy", ValueName = "ene", UnitOfMeasures = "kWh" };

        public static DeviceClassDescription Voltage => new()
        { DeviceClass = "voltage", ValueName = "volt", UnitOfMeasures = "V" };

        public static DeviceClassDescription Current => new()
        { DeviceClass = "current", ValueName = "amps", UnitOfMeasures = "A" };

        public static DeviceClassDescription Power => new()
        { DeviceClass = "power", ValueName = "watt", UnitOfMeasures = "W" };

        public static DeviceClassDescription PowerFactor => new()
        { DeviceClass = "power_factor", ValueName = "pfact", UnitOfMeasures = "%" };

        public static DeviceClassDescription Frequency => new()
        { DeviceClass = "frequency", ValueName = "freq", UnitOfMeasures = "Hz" };

        public static DeviceClassDescription PressureHpa => new()
        { DeviceClass = "pressure", ValueName = "pres", UnitOfMeasures = "hPa" };

        public static DeviceClassDescription PressureMbar => new()
        { DeviceClass = "pressure", ValueName = "pres", UnitOfMeasures = "mbar" };
    }
}
