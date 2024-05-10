using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceBaseWorkers
{
    public class DeviceBaseWorker<T>(string deviceId, ILogger<T> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IMqttClientForMultipleSubscribers mqttClient) : BackgroundService, IMqttSubscriber
    {
        protected ILogger<T> Logger { get; } = logger;
        protected WorkersConfiguration WorkersConfiguration { get; } = workersConfiguration.Value;
        protected MqttConfiguration MqttConfiguration { get; } = mqttConfiguration.Value;
        protected IMqttClientForMultipleSubscribers MqttClient { get; set; } = mqttClient;

        // service tasks, that we start to work
        protected List<Task> WorkingTasks { get; set; } = [];

        protected virtual string DeviceId { get; } = deviceId;

        protected List<IHassComponent> ComponentList { get; } = [];

        public virtual async Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs e) =>
            await Task.CompletedTask;

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
                    _ = Task.WaitAll([.. WorkingTasks], 5000, stoppingToken);
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
                        JsonSerializer.Serialize((object)component), // need to convert to object because Text.Json does not support inheritance
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

        protected virtual JsonObject CreatePayloadObject() => new() { { "Id", DeviceId }, { "name", $"{DeviceId}" } };

        protected virtual async Task SendDeviceInformation(IHassComponent component, JsonObject payload)
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
