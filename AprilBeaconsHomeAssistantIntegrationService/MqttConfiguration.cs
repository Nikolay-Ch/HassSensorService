using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;
using MQTTnet.Protocol;
using System.Security.Cryptography;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public class MqttConfiguration
    {
        // create ClientId: GUID in windows systems, and /etc/machine-id in linux
        public string ClientId { get; set; } = new DeviceIdBuilder()
            .AddOSInstallationID()
            .UseFormatter(new StringDeviceIdFormatter(new PlainTextDeviceIdComponentEncoder()))
            .ToString();
        public string MqttUri { get; set; }
        public string MqttUser { get; set; }
        public string MqttUserPassword { get; set; }
        public int MqttPort { get; set; } = 1883;
        public bool MqttSecure { get; set; } = false;
        public MqttQualityOfServiceLevel MqttQosLevel { get; set; } = MqttQualityOfServiceLevel.AtMostOnce;
        public string TopicBase { get; set; }
        public string TopicToSubscribe { get; set; }
        public string ConfigurationTopic { get; set; }
    }
}
