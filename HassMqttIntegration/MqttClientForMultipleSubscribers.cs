﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HassMqttIntegration
{
    public interface IMqttClientForMultipleSubscribers
    {
        bool IsMqttConnected { get; }

        Task PublishAsync(string topic, string payload, MqttQualityOfServiceLevel mqttQosLevel);
        Task PublishAsync(string configurationTopic, string configurationPayload, MqttQualityOfServiceLevel mqttQosLevel, bool retain);
        Task SubscribeAsync(IMqttSubscriber subscriber, string v, MqttQualityOfServiceLevel mqttQosLevel);
        Task UnsubscribeAsync(IMqttSubscriber subscriber, string v);
    }

    public class MqttClientForMultipleSubscribers : IMqttClientForMultipleSubscribers
    {
        protected ILogger<MqttClientForMultipleSubscribers> Logger { get; }
        protected MqttConfiguration MqttConfiguration { get; }
        protected List<KeyValuePair<string, List<IMqttSubscriber>>> Subscribers { get; set; } = [];

        public IManagedMqttClient MqttClient { get; }

        public bool IsMqttConnected => MqttClient.IsConnected;

        public MqttClientForMultipleSubscribers(ILogger<MqttClientForMultipleSubscribers> logger, IOptions<MqttConfiguration> mqttConfiguration)
        {
            Logger = logger;
            MqttConfiguration = mqttConfiguration.Value;

            Logger.LogInformation("Creating MqttClient at: {time}. Uri:{uri}", DateTimeOffset.Now, MqttConfiguration.MqttUri);

            var tlsOptions = new MqttClientTlsOptions()
            {
                SslProtocol = MqttConfiguration.MqttSecure ? SslProtocols.Tls12 : SslProtocols.None,
                IgnoreCertificateChainErrors = true,
                IgnoreCertificateRevocationErrors = true
            };

            var options = new MqttClientOptionsBuilder()
                .WithClientId(MqttConfiguration.ClientId.Replace("-", "").Replace(" ", ""))
                .WithCredentials(MqttConfiguration.MqttUser, MqttConfiguration.MqttUserPassword)
                .WithTcpServer(MqttConfiguration.MqttUri, MqttConfiguration.MqttPort)
                .WithCleanSession()
                .WithTlsOptions(tlsOptions)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();

            MqttClient = new MqttFactory()
                .CreateManagedMqttClient();

            MqttClient.StartAsync(managedOptions).Wait();

            // wait for connection
            while (!MqttClient.IsConnected)
            {
                Logger.LogTrace("MqttClient not connected... Go to sleep for a second...");
                Thread.Sleep(1000);
            }

            MqttClient.ApplicationMessageReceivedAsync += async e => await MessageReceive(e);

            Logger.LogInformation("Creating MqttClient done at: {time}", DateTimeOffset.Now);
        }

        // receive messaged from all subscribed topics
        private async Task MessageReceive(MqttApplicationMessageReceivedEventArgs e)
        {
            foreach (var topicSubscribers in Subscribers)
            {
                // call each subscriber, that topic correspond to received message topic
                var pattern = "^" + topicSubscribers.Key.Replace("+", ".*") + "$";
                if (Regex.IsMatch(e.ApplicationMessage.Topic, pattern))
                    foreach (var subscriber in topicSubscribers.Value)
                        await subscriber.MqttReceiveHandler(e);
            }
        }

        public async Task PublishAsync(string topic, string payload, MqttQualityOfServiceLevel mqttQosLevel) =>
            await PublishAsync(topic, payload, mqttQosLevel, false);

        public async Task PublishAsync(string topic, string payload, MqttQualityOfServiceLevel mqttQosLevel, bool retain) =>
            await MqttClient.EnqueueAsync(topic, payload, mqttQosLevel, retain);

        public async Task SubscribeAsync(IMqttSubscriber subscriber, string topic, MqttQualityOfServiceLevel mqttQosLevel)
        {
            if (!Subscribers.Any(e => e.Key == topic))
                Subscribers.Add(new KeyValuePair<string, List<IMqttSubscriber>>(topic, []));

            var topicSubscribers = Subscribers.First(e => e.Key == topic).Value;

            // client already subscribed to this topic
            if (!topicSubscribers.Contains(subscriber))
            {
                await MqttClient.SubscribeAsync(topic, mqttQosLevel);
                topicSubscribers.Add(subscriber);
            }
        }

        public async Task UnsubscribeAsync(IMqttSubscriber subscriber, string topic)
        {
            if (!Subscribers.Any(e => e.Key == topic))
                Subscribers.Remove(new KeyValuePair<string, List<IMqttSubscriber>>(topic, []));

            await MqttClient.UnsubscribeAsync(topic);
        }
    }
}
