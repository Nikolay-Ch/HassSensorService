using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceBaseWorkers
{
    public class DeviceBaseWorker<T>(
        IMemoryCache cache,
        IReadOnlyDictionary<string, string> workerParameters,
        ILogger<T> logger, IOptions<WorkersConfiguration> workersConfiguration,
        IOptions<MqttConfiguration> mqttConfiguration,
        IMqttClientForMultipleSubscribers mqttClient) : BackgroundService, IMqttSubscriber
    {
        protected IMemoryCache Cache { get; } = cache;
        protected ILogger<T> Logger { get; } = logger;
        protected WorkersConfiguration WorkersConfiguration { get; } = workersConfiguration.Value;
        protected MqttConfiguration MqttConfiguration { get; } = mqttConfiguration.Value;
        protected IMqttClientForMultipleSubscribers MqttClient { get; set; } = mqttClient;

        // service tasks, that we start to work
        protected List<Task> WorkingTasks { get; set; } = [];

        protected virtual string DeviceId { get; } = workerParameters.ContainsKey("DeviceId") ? workerParameters["DeviceId"] : "";

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

        protected string GetValueName(string valueName) => ComponentList
                                            .Single(e => e.DeviceClassDescription.ValueName == valueName)
                                            .DeviceClassDescription
                                            .ValueName;

        protected virtual JsonCachedPayload CreatePayloadObject(JsonObject? payload = null) =>
            new(DeviceId, Cache, payload ?? new JsonObject() { { "Id", DeviceId }, { "name", $"{DeviceId}" } }, Logger);

        protected virtual async Task SendDeviceInformation(string topic, JsonCachedPayload payload)
        {
            try
            {
                Logger.LogTrace("SendDeviceInformation: Method starts... {type}, MqttClient is connected: {connected}",
                    typeof(T).Name, MqttClient.IsMqttConnected);

                // send message
                await MqttClient.PublishAsync(
                    topic.Replace("+/+", $"{MqttConfiguration.MqttHomeAssistantHomeTopic}/{WorkersConfiguration.ServiceName}"),
                    payload.ToString(),
                    MqttConfiguration.MqttQosLevel);

                Logger.LogInformation("{type} send information message: {topic} - {payload} at {time}",
                    typeof(T).Name, topic, payload, DateTimeOffset.Now);

                Logger.LogTrace("SendDeviceInformation: Method ends... {type}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{type} error at {time}", typeof(T).Name, DateTimeOffset.Now);
            }
        }
    }
}
