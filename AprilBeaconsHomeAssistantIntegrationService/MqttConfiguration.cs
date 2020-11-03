using MQTTnet.Protocol;
using System;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public class MqttConfiguration
    {
        public string ClientId { get; set; } = Guid.NewGuid().ToString();
        public string MqttUri { get; set; }
        public string MqttUser { get; set; }
        public string MqttUserPassword { get; set; }
        public int MqttPort { get; set; } = 1883;
        public bool MqttSecure { get; set; } = false;
        public MqttQualityOfServiceLevel MqttQosLevel { get; set; } = MqttQualityOfServiceLevel.AtMostOnce;
        public string TopicBase { get; set; }
    }
}
