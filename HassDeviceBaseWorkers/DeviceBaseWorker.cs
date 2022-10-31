using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceBaseWorkers
{
    public class DeviceBaseWorker<T> : BackgroundService, IMqttSubscriber
    {
        protected ILogger<T> Logger { get; }
        protected WorkersConfiguration WorkersConfiguration { get; }
        protected MqttConfiguration MqttConfiguration { get; }
        protected IMqttClientForMultipleSubscribers MqttClient { get; set; }

        // service tasks, that we start to work
        protected List<Task> WorkingTasks { get; set; } = new List<Task>();

        protected virtual string DeviceId { get; }

        protected List<IHassComponent> ComponentList { get; } = new List<IHassComponent>();

        public virtual async Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs e) =>
            await Task.CompletedTask;

        public DeviceBaseWorker(string deviceId, ILogger<T> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IMqttClientForMultipleSubscribers mqttClient)
        {
            DeviceId = deviceId;
            Logger = logger;
            WorkersConfiguration = workersConfiguration.Value;
            MqttConfiguration = mqttConfiguration.Value;
            MqttClient = mqttClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await PreSendConfigurationAsync(stoppingToken);

                // send device configuration with retain flag
                await SendDeviceConfiguration();
                Logger.LogInformation("{type} sent devices configuration at: {time}", typeof(T).Name, DateTimeOffset.Now);

                await PostSendConfigurationAsync(stoppingToken);

                Logger.LogInformation("{type} running at: {time}", typeof(T).Name, DateTimeOffset.Now);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                        await Task.Delay(30000, stoppingToken);

                    // wait to all work tasks finished
                    Task.WaitAll(WorkingTasks.ToArray(), 5000, stoppingToken);
                }
                catch (TaskCanceledException) { }

                await BeforeExitAsync();

                Logger.LogInformation("{type} stopping at: {time}", typeof(T).Name, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{type} error at {time}", typeof(T).Name, DateTimeOffset.Now);
            }
        }

        protected virtual async Task PostSendConfigurationAsync(CancellationToken stoppingToken) => await Task.CompletedTask;

        protected virtual async Task PreSendConfigurationAsync(CancellationToken stoppingToken) => await Task.CompletedTask;

        protected virtual async Task BeforeExitAsync() => await Task.CompletedTask;

        /// <summary>
        /// publish configuration message with retain flag to HomeAssistant
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected virtual async Task SendDeviceConfiguration()
        {
            try
            {
                foreach (var component in ComponentList)
                {
                    await MqttClient.PublishAsync(
                        $"{MqttConfiguration.ConfigurationTopicBase}/" +
                        $"{component.GetType().GetHassComponentTypeString()}/" +
                        $"{component.UniqueId}/config",
                        JsonConvert.SerializeObject(component),
                        MqttConfiguration.MqttQosLevel,
                        true);

                    Logger.LogInformation("{type} send configuration for component {component} at: {time}",
                        typeof(T).Name, component.UniqueId, DateTimeOffset.Now);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{type} error at {time}", typeof(T).Name, DateTimeOffset.Now);
            }
        }

        protected virtual JObject CreatePayloadObject() =>
            JObject.FromObject(new { Id = DeviceId, name = $"{DeviceId}" });

        protected virtual async Task SendDeviceInformation(IHassComponent component, JObject payload)
        {
            try
            {
                Logger.LogTrace("SendDeviceInformation: Method starts... {type}, MqttClient is connected: {connected}",
                    typeof(T).Name, MqttClient.IsMqttConnected);

                // send message
                await MqttClient.PublishAsync(
                    component
                        .StateTopic
                        .Replace("+/+", $"{MqttConfiguration.MqttHomeAssistantHomeTopic}/{WorkersConfiguration.ServiceName}"),
                    payload.ToString(),
                    MqttConfiguration.MqttQosLevel);

                Logger.LogInformation("{type} send information message: {payload} at {time}",
                    typeof(T).Name, payload, DateTimeOffset.Now);

                Logger.LogTrace("SendDeviceInformation: Method ends... {type}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{type} error at {time}", typeof(T).Name, DateTimeOffset.Now);
            }
        }
    }
}
