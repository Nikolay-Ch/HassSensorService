using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;
using MQTTnet.Protocol;
using System;

namespace HassMqttIntegration
{
    public class MqttConfiguration
    {
        protected class GuidDeviceIdComponent : IDeviceIdComponent
        {
            public string GetValue() => Guid.NewGuid().ToString("N");
        }

        // create ClientId: GUID in windows systems, and /etc/machine-id in linux
        public string ClientId { get; set; } = new DeviceIdBuilder()
            .AddMachineName()
            .AddOsVersion()
            .AddComponent("guid", new GuidDeviceIdComponent())
            .UseFormatter(new StringDeviceIdFormatter(new PlainTextDeviceIdComponentEncoder()))
            .ToString();

        public string MqttUri { get; set; }
        public string MqttUser { get; set; }
        public string MqttUserPassword { get; set; }
        public int MqttPort { get; set; } = 1883;
        public bool MqttSecure { get; set; } = false;
        public MqttQualityOfServiceLevel MqttQosLevel { get; set; } = MqttQualityOfServiceLevel.AtMostOnce;
        public string TopicToSubscribe { get; set; }
        public string ConfigurationTopicBase { get; set; }
        public string MqttHomeAssistantHomeTopic { get; set; }
    }
}
