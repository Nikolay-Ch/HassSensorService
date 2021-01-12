using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public static class SensorClassNameExtensions
    {
        private static SensorClassDescriptionAttribute GetValueAttribute(SensorClass sensorClass) =>
            (SensorClassDescriptionAttribute)typeof(SensorClass).GetMember(sensorClass.ToString())[0].GetCustomAttributes(typeof(SensorClassDescriptionAttribute), false)[0];

        public static string SensorCategory(this SensorClass sensorClass) => GetValueAttribute(sensorClass).SensorCategory;
        public static string ValueName(this SensorClass sensorClass) => GetValueAttribute(sensorClass).ValueName;
        public static string UnitOfMeasures(this SensorClass sensorClass) => GetValueAttribute(sensorClass).UnitOfMeasures;
    }

    /*public enum SensorCategory
    {
        sensor,
        binary_sensor
    }*/

    public sealed class SensorClassDescriptionAttribute : Attribute
    {
        public SensorClassDescriptionAttribute(string sensorCategory, string sensorShortNameForId, string unitOfMeasures)
        {
            SensorCategory = sensorCategory;
            ValueName = sensorShortNameForId;
            UnitOfMeasures = unitOfMeasures;
        }

        public string SensorCategory { get; }
        public string ValueName { get; }
        public string UnitOfMeasures { get; }
    }

    public enum SensorClass
    {
        [SensorClassDescription("sensor", "temp", "°C")]
        temperature,

        [SensorClassDescription("sensor", "hum", "%")]
        humidity,

        [SensorClassDescription("sensor", "lux", "lx")]
        illuminance,

        [SensorClassDescription("sensor", "batt", "%")]
        battery,

        [SensorClassDescription("sensor", "moi", "%")]
        moisture,

        [SensorClassDescription("sensor", "fert", "µS/cm")]
        fertility
    }

    public class Sensor
    {
        protected const string HomeAssistatnStateTopicName = "BTtoMQTT";

        [JsonProperty("stat_t")]
        public string StatTopic => $"+/+/{HomeAssistatnStateTopicName}/{Device.Identifiers[0]}";

        [JsonProperty("name")]
        public string SensorName => $"{Device.Name}-{SensorClass.ValueName()}";

        [JsonProperty("uniq_id")]
        public string UniqueId => $"{Device.Identifiers[0]}-{Device.Name}-{SensorClass.ValueName()}";

        [JsonProperty("dev_cla")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SensorClass SensorClass { get; set; }

        [JsonProperty("ic", NullValueHandling = NullValueHandling.Ignore)]
        public string SensorIcon { get; set; }

        [JsonProperty("val_tpl")]
        public string ValueJScript => $"{{{{ value_json.{SensorClass.ValueName()} | is_defined }}}}";

        [JsonProperty("unit_of_meas")]
        public string UnitOfMeasures => SensorClass.UnitOfMeasures();

        [JsonProperty("device")]
        public Device Device { get; set; }
    }

    public class Device
    {
        [JsonProperty("connections")]
        public List<List<string>> Connections { get; set; } = new List<List<string>>();

        [JsonProperty("identifiers")]
        public List<string> Identifiers { get; set; } = new List<string>();

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("via_device")]
        public string ViaDevice { get; set; }
    }
}
