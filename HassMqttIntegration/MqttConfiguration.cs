using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;
using MQTTnet.Protocol;
using System;

namespace HassMqttIntegration
{
    public record class MqttConfiguration
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

        public required string MqttUri { get; set; }
        public required string MqttUser { get; set; }
        public required string MqttUserPassword { get; set; }
        public required int MqttPort { get; set; } = 1883;
        public required bool MqttSecure { get; set; } = false;
        public required MqttQualityOfServiceLevel MqttQosLevel { get; set; } = MqttQualityOfServiceLevel.AtMostOnce;
        public required string TopicToSubscribe { get; set; }
        public required string ConfigurationTopicBase { get; set; }
        public required string MqttHomeAssistantHomeTopic { get; set; }
    }
}
