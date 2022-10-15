using MQTTnet.Client;
using System.Threading.Tasks;

namespace HassMqttIntegration
{
    public interface IMqttSubscriber
    {
        Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs args);
    }
}
