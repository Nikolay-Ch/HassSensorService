using MQTTnet;
using System.Threading.Tasks;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public interface IMqttSubscriber
    {
        Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs e);
    }
}
