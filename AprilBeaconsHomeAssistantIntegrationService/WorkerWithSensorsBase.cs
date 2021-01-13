using HassSensorConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public class WorkerWithSensorsBase<T> : BackgroundService, IMqttSubscriber
    {
        protected ILogger<T> Logger { get; }
        protected MqttConfiguration MqttConfiguration { get; }
        protected ProgramConfiguration ProgramConfiguration { get; set; }
        protected IMqttClientForMultipleSubscribers MqttClient { get; set; }

        // service tasks, that we start to work
        protected List<Task> WorkingTasks { get; set; } = new List<Task>();

        protected virtual List<string> Devices { get; } = new List<string>();

        protected virtual List<Sensor> SensorsList { get; } = new List<Sensor>();

        public virtual async Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs e) =>
            await Task.CompletedTask;

        public WorkerWithSensorsBase(ILogger<T> logger, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ProgramConfiguration> programConfiguration, IMqttClientForMultipleSubscribers mqttClient)
        {
            Logger = logger;
            MqttConfiguration = mqttConfiguration.Value;
            ProgramConfiguration = programConfiguration.Value;
            MqttClient = mqttClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await PreExecuteAsync(stoppingToken);

                // send device configuration with retain flag
                await SendSensorConfiguration();
                Logger.LogInformation($"{typeof(T).Name} sent sensors configuration at: {DateTimeOffset.Now}");

                await PostExecuteAsync(stoppingToken);

                Logger.LogInformation($"{typeof(T).Name} running at: {DateTimeOffset.Now}");

                while (!stoppingToken.IsCancellationRequested)
                    await Task.Delay(1000, stoppingToken);

                // wait to all work tasks finished
                Task.WaitAll(WorkingTasks.ToArray(), 5000);

                Logger.LogInformation($"{typeof(T).Name} stopping at: {DateTimeOffset.Now}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{typeof(T).Name} error at {DateTimeOffset.Now}");
            }
        }

        protected virtual async Task PostExecuteAsync(CancellationToken stoppingToken) => await Task.CompletedTask;

        protected virtual async Task PreExecuteAsync(CancellationToken stoppingToken) => await Task.CompletedTask;

        /// <summary>
        /// publish configuration message with retain flag to HomeAssistant
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected virtual async Task SendSensorConfiguration()
        {
            try
            {
                foreach (var deviceId in Devices)
                    foreach (var sensor in SensorsList)
                    {
                        FillSensorIdData(sensor, deviceId);

                        await MqttClient.PublishAsync(
                            string.Format(MqttConfiguration.ConfigurationTopic, sensor.SensorClass.SensorCategory(), sensor.UniqueId),
                            JsonConvert.SerializeObject(sensor),
                            MqttConfiguration.MqttQosLevel,
                            true);

                        Logger.LogInformation($"{typeof(T).Name} send configuration for sensor {sensor.UniqueId} at: {DateTimeOffset.Now}");

                        // clear values because of sharing single object...
                        ClearSensorIdData(sensor, deviceId);
                    }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{typeof(T).Name} error at {DateTimeOffset.Now}");
            }
        }

        private void FillSensorIdData(Sensor sensor, string deviceId)
        {
            sensor.Device.Identifiers.Add(deviceId);
            sensor.Device.Connections.Add(new List<string> { "mac", deviceId });
        }

        private void ClearSensorIdData(Sensor sensor, string deviceId)
        {
            sensor.Device.Identifiers.Clear();
            sensor.Device.Connections.Clear();
        }
    }
}
