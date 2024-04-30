namespace HassSensorConfiguration
{
    public class DeviceClassDescription
    {
        public string DeviceClass { get; init; } // name of device class. This property has null-value for non-standard device-classes
        public string ValueName { get; init; } // name of property, contained value in value-MQTT message
        public string UnitOfMeasures { get; init; } // contains unit of measures in human-friendly text
        public IHassComponentFactory ComponentFactory { get; init; } // reference to factory, that creating this type of sensor

        public DeviceClassDescription() { }

        public DeviceClassDescription(DeviceClassDescription deviceClassDescription)
        {
            DeviceClass = deviceClassDescription.DeviceClass;
            ValueName = deviceClassDescription.ValueName;
            UnitOfMeasures = deviceClassDescription.UnitOfMeasures;
        }

        public static AnalogSensorFactory AnalogSensorFactory { get; } = new();
        public static BinarySensorFactory BinarySensorFactory { get; } = new();

        public static DeviceClassDescription None => new() { ValueName = "state" };

        #region AnalogSensor
        public static DeviceClassDescription ApparentPower => new()
        { DeviceClass = "apparent_power", ValueName = "va", UnitOfMeasures = "VA", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Aqi => new()
        { DeviceClass = "aqi", ValueName = "aqi", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Battery => new()
        { DeviceClass = "battery", ValueName = "batt", UnitOfMeasures = "%", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription CarbonDioxide => new()
        { DeviceClass = "carbon_dioxide", ValueName = "co2", UnitOfMeasures = "ppm", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription CarbonMonoxide => new()
        { DeviceClass = "carbon_monoxide", ValueName = "co", UnitOfMeasures = "ppm", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Current => new()
        { DeviceClass = "current", ValueName = "amps", UnitOfMeasures = "A", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Date => new()
        { DeviceClass = "date", ValueName = "dt", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription EnergyWh => new()
        { DeviceClass = "energy", ValueName = "ewh", UnitOfMeasures = "Wh", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription EnergyKWh => new()
        { DeviceClass = "energy", ValueName = "ekwh", UnitOfMeasures = "kWh", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription EnergyMWh => new()
        { DeviceClass = "energy", ValueName = "emwh", UnitOfMeasures = "MWh", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Fertility => new()
        { ValueName = "fer", UnitOfMeasures = "µS/cm", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription FrequencyHz => new()
        { DeviceClass = "frequency", ValueName = "freqh", UnitOfMeasures = "Hz", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription FrequencyKHz => new()
        { DeviceClass = "frequency", ValueName = "freqkh", UnitOfMeasures = "kHz", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription FrequencyMHz => new()
        { DeviceClass = "frequency", ValueName = "freqmh", UnitOfMeasures = "MHz", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription FrequencyGHz => new()
        { DeviceClass = "frequency", ValueName = "freqgz", UnitOfMeasures = "GHz", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Gas => new()
        { DeviceClass = "gas", ValueName = "gas", UnitOfMeasures = "m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Humidity => new()
        { DeviceClass = "humidity", ValueName = "hum", UnitOfMeasures = "%", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription IlluminanceLux => new()
        { DeviceClass = "illuminance", ValueName = "lux", UnitOfMeasures = "lx", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription IlluminanceLum => new()
        { DeviceClass = "illuminance", ValueName = "lm", UnitOfMeasures = "lm", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Moisture => new()
        { ValueName = "moi", UnitOfMeasures = "%", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Monetary => new()
        { ValueName = "monet", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription NitrogenDioxide => new()
        { DeviceClass = "nitrogen_dioxide", ValueName = "no2", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription NitrogenMonoxide => new()
        { DeviceClass = "nitrogen_monoxide", ValueName = "no", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription NitrousOxide => new()
        { DeviceClass = "nitrous_oxide", ValueName = "n2o", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Ozone => new()
        { DeviceClass = "ozone", ValueName = "o3", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription ParticulateMatter1 => new()
        { DeviceClass = "pm1", ValueName = "pm1", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription ParticulateMatter10 => new()
        { DeviceClass = "pm10", ValueName = "pm10", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription ParticulateMatter25 => new()
        { DeviceClass = "pm25", ValueName = "pm25", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription PowerFactor => new()
        { DeviceClass = "power_factor", ValueName = "pfact", UnitOfMeasures = "%", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Power => new()
        { DeviceClass = "power", ValueName = "watt", UnitOfMeasures = "W", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription PressureHpa => new()
        { DeviceClass = "pressure", ValueName = "pres", UnitOfMeasures = "hPa", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription ReactivePower => new()
        { DeviceClass = "reactive_power", ValueName = "var", UnitOfMeasures = "var", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription SignalStrengthDb => new()
        { DeviceClass = "signal_strength", ValueName = "db", UnitOfMeasures = "dB", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription SignalStrengthDbm => new()
        { DeviceClass = "signal_strength", ValueName = "dbm", UnitOfMeasures = "dBm", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription SulphurDioxide => new()
        { DeviceClass = "sulphur_dioxide", ValueName = "so2", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Temperature => new()
        { DeviceClass = "temperature", ValueName = "temp", UnitOfMeasures = "°C", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription TemperatureCelsius => new()
        { DeviceClass = "temperature", ValueName = "tempc", UnitOfMeasures = "°C", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Timestamp => new()
        { DeviceClass = "timestamp", ValueName = "ts", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription VolatileOrganicCompounds => new()
        { DeviceClass = "volatile_organic_compounds", ValueName = "voc", UnitOfMeasures = "µg/m³", ComponentFactory = AnalogSensorFactory };
        public static DeviceClassDescription Voltage => new()
        { DeviceClass = "voltage", ValueName = "volt", UnitOfMeasures = "V", ComponentFactory = AnalogSensorFactory };
        #endregion

        #region BinarySensors
        public static DeviceClassDescription BatteryBinary => new() { DeviceClass = "battery", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription BatteryCharging => new() { DeviceClass = "battery_charging", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription CarbonMonoxideBinary => new() { DeviceClass = "carbon_monoxide", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Cold => new() { DeviceClass = "cold", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Connectivity => new() { DeviceClass = "connectivity", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Door => new() { DeviceClass = "door", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription GarageDoor => new() { DeviceClass = "garage_door", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription GasBinary => new() { DeviceClass = "gas", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Heat => new() { DeviceClass = "heat", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Light => new() { DeviceClass = "light", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Lock => new() { DeviceClass = "lock", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription MoistureBinary => new() { DeviceClass = "moisture", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Motion => new() { DeviceClass = "motion", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Moving => new() { DeviceClass = "moving", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Occupancy => new() { DeviceClass = "occupancy", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Opening => new() { DeviceClass = "opening", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Plug => new() { DeviceClass = "plug", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription PowerBinary => new() { DeviceClass = "power", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Presence => new() { DeviceClass = "presence", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Problem => new() { DeviceClass = "problem", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Running => new() { DeviceClass = "running", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Safety => new() { DeviceClass = "safety", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Smoke => new() { DeviceClass = "smoke", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Tampering => new() { DeviceClass = "tamper", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Update => new() { DeviceClass = "update", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Vibration => new() { DeviceClass = "vibration", ValueName = "state", ComponentFactory = BinarySensorFactory };
        public static DeviceClassDescription Window => new() { DeviceClass = "window", ValueName = "state", ComponentFactory = BinarySensorFactory };
        #endregion
    }
}
