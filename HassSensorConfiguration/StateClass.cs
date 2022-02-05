using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace HassSensorConfiguration
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StateClass
    {
        None = 0,

        [EnumMember(Value = "measurement")]
        Measurement = 1,

        [EnumMember(Value = "total_increasing")]
        TotalIncreasing = 2
    }

    /*public class StateClassHassConveter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => existingValue;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(((StateClass)value)..ToString().ToLower());

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                o.WriteTo(writer);
            }
        }
    }*/
}
