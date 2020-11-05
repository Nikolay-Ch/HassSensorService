using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public interface IMqttClientForMultipleSubscribers
    {
        Task PublishAsync(string topic, string payload, MqttQualityOfServiceLevel mqttQosLevel);
        Task PublishAsync(string configurationTopic, string configurationPayload, MqttQualityOfServiceLevel mqttQosLevel, bool v);
        Task SubscribeAsync(IMqttSubscriber subscriber, string v, MqttQualityOfServiceLevel mqttQosLevel);
    }

    class MqttClientForMultipleSubscribers : IMqttClientForMultipleSubscribers
    {
        protected ILogger<MqttClientForMultipleSubscribers> Logger { get; }
        protected MqttConfiguration MqttConfiguration { get; }
        protected ProgramConfiguration ProgramConfiguration { get; set; }
        protected List<KeyValuePair<string, List<IMqttSubscriber>>> Subscribers { get; set; } =
            new List<KeyValuePair<string, List<IMqttSubscriber>>>();

        public IManagedMqttClient MqttClient { get; }

        public MqttClientForMultipleSubscribers(ILogger<MqttClientForMultipleSubscribers> logger, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ProgramConfiguration> programConfiguration)
        {
            Logger = logger;
            MqttConfiguration = mqttConfiguration.Value;
            ProgramConfiguration = programConfiguration.Value;

            Logger.LogInformation("Creating MqttClient at: {time}", DateTimeOffset.Now);

            var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(MqttConfiguration.ClientId.Replace("-", ""))
                .WithCredentials(MqttConfiguration.MqttUser, MqttConfiguration.MqttUserPassword)
                .WithTcpServer(MqttConfiguration.MqttUri, MqttConfiguration.MqttPort)
                .WithCleanSession();

            var options = MqttConfiguration.MqttSecure ?
                messageBuilder.WithTls().Build() :
                messageBuilder.Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();

            MqttClient = new MqttFactory()
                .CreateManagedMqttClient();

            MqttClient.StartAsync(managedOptions).Wait();

            // wait for connection
            while (!MqttClient.IsConnected)
                Thread.Sleep(1000);

            MqttClient.UseApplicationMessageReceivedHandler(async e => await MessageReceive(e));

            Logger.LogInformation("Creating MqttClient done at: {time}", DateTimeOffset.Now);
        }
            
        // receive messaged from all subscribed topics
        private async Task MessageReceive(MqttApplicationMessageReceivedEventArgs e)
        {
            foreach(var topicSubscribers in Subscribers)
            {
                var topicValue =
                    topicSubscribers.Key.EndsWith("#") ?
                        topicSubscribers.Key.Remove(topicSubscribers.Key.Length - 1) :
                        topicSubscribers.Key;

                // call each subscriber, that topic correspond to received message topic
                if (e.ApplicationMessage.Topic.StartsWith(topicValue))
                    foreach (var subscriber in topicSubscribers.Value)
                        await subscriber.MqttReceiveHandler(e);
            }
        }

        public async Task PublishAsync(string topic, string payload, MqttQualityOfServiceLevel mqttQosLevel) =>
            await MqttClient.PublishAsync(topic, payload, mqttQosLevel);

        public async Task PublishAsync(string topic, string payload, MqttQualityOfServiceLevel mqttQosLevel, bool retain) =>
            await MqttClient.PublishAsync(topic, payload, mqttQosLevel, retain);

        public async Task SubscribeAsync(IMqttSubscriber subscriber, string topic, MqttQualityOfServiceLevel mqttQosLevel)
        {
            if (!Subscribers.Any(e => e.Key == topic))
                Subscribers.Add(new KeyValuePair<string, List<IMqttSubscriber>>(topic, new List<IMqttSubscriber>()));

            var topicSubscribers = Subscribers.First(e => e.Key == topic).Value;

            // client already subscribed to this topic
            if (!topicSubscribers.Contains(subscriber))
            {
                await MqttClient.SubscribeAsync(topic, mqttQosLevel);
                topicSubscribers.Add(subscriber);
            }
        }
    }
}
