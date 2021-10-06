using MQTTnet;
using System.Threading.Tasks;

namespace HassMqttIntegration
{
    public interface IMqttSubscriber
    {
        Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs e);
    }
}
