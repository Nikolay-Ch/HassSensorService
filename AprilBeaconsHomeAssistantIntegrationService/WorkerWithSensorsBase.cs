using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
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

        protected virtual List<Sensor> SensorsList { get; } = new List<Sensor>();

        protected virtual string HomeAssistantDeviceConfigurationTopicPattern => "homeassistant/sensor/{0}/config";

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
            await PreExecuteAsync(stoppingToken);

            // send device configuration with retain flag
            await SendSensorConfiguration();
            Logger.LogInformation($"{typeof(T).Name} sent sensors configuration at: {{time}}", DateTimeOffset.Now);

            await PostExecuteAsync(stoppingToken);

            Logger.LogInformation($"{typeof(T).Name} running at: {{time}}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(1000, stoppingToken);

            Logger.LogInformation($"{typeof(T).Name} stopping at: {{time}}", DateTimeOffset.Now);
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
            foreach (var device in ProgramConfiguration.AprilBeaconDevicesList)
                foreach (var sensor in SensorsList)
                {
                    var name = string.Format(sensor.NamePattern, device);
                    var uniqueId = string.Format(sensor.UniqueIdPattern, device);

                    var configurationPayload = JObject.FromObject(new
                    {
                        stat_t = $"{MqttConfiguration.TopicBase}/{device}",
                        name,
                        uniq_id = uniqueId,
                        dev_cla = sensor.Class,
                        val_tpl = $"{{{{ value_json.{sensor.Value} | is_defined }}}}",
                        unit_of_meas = sensor.Unit
                    });

                    await MqttClient.PublishAsync(
                        String.Format(HomeAssistantDeviceConfigurationTopicPattern, uniqueId),
                        configurationPayload.ToString(),
                        MqttConfiguration.MqttQosLevel, true);
                }
        }
    }
}
