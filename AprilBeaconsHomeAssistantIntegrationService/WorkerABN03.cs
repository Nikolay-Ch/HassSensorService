using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    /// <summary>
    /// Convert advertising raw-data into normal values for HomeAssistant MQTT (with autodiscover option)
    /// see documentation of the N03 April Beacon device
    /// https://wiki.aprbrother.com/en/ABSensor.html#absensor-n03
    /// </summary>
    public class WorkerABN03 : BackgroundService
    {
        protected ILogger<WorkerABN03> Logger { get; }
        protected MqttConfiguration MqttConfiguration { get; }
        protected ProgramConfiguration ProgramConfiguration { get; set; }

        protected List<Sensor> SensorsList { get; } = new List<Sensor>
        {
            new Sensor { NamePattern = "ABN03_tem", UniqueIdPattern = "{0}-ABN03-tem", Class = "temperature", Value = "temp", Unit = "C" },
            new Sensor { NamePattern = "ABN03_hum", UniqueIdPattern = "{0}-ABN03-hum", Class = "humidity", Value = "hum", Unit = "%" },
            new Sensor { NamePattern = "ABN03_lux", UniqueIdPattern = "{0}-ABN03-lux", Class = "illuminance", Value = "lux", Unit = "lx" },
            new Sensor { NamePattern = "ABN03_batt", UniqueIdPattern = "{0}-ABN03-batt", Class = "battery", Value = "batt", Unit = "%" },
        };

        public WorkerABN03(ILogger<WorkerABN03> logger, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ProgramConfiguration> programConfiguration)
        {
            Logger = logger;
            MqttConfiguration = mqttConfiguration.Value;
            ProgramConfiguration = programConfiguration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(MqttConfiguration.ClientId)
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

            var client = new MqttFactory()
                .CreateManagedMqttClient();

            await client.StartAsync(managedOptions);

            // wait for connection
            while (!client.IsConnected)
                Thread.Sleep(1000);

            client.UseApplicationMessageReceivedHandler(async e => await ConvertSensorDataToHomeAssistant(e, client));

            // subscribe for all devices
            await SubscribeToAllDevices(client);

            // send device configuration
            await SendSensorConfiguration(client);

            Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(1000, stoppingToken);

            Logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
        }

        private async Task ConvertSensorDataToHomeAssistant(MqttApplicationMessageReceivedEventArgs e, IManagedMqttClient client)
        {
            try
            {
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var payloadObj = JObject.Parse(payload);
                var serviceData = payloadObj.ContainsKey("servicedata") ? payloadObj["servicedata"].Value<string>() : "";

                // if serviceData not started with ab03, that we can't parse data - skip parsing
                if (!serviceData.StartsWith("ab03"))
                    return;

                // convert hexadecimal string info byte-array
                var payloadBytes = serviceData.FromHexStringToByteArray();

                // create parsed messages
                var temp = payloadBytes.ParseShortLE(9, 8);
                var humi = payloadBytes.ParseShortLE(11, 2);
                var illu = payloadBytes.ParseTwoBytesLE(13);
                var batt = payloadBytes[8];

                // add values into dictionary to easy search by sensor-type
                var vals = new Dictionary<Sensor, object>
                {
                    { SensorsList.First(e => e.Class == "temperature"), temp },
                    { SensorsList.First(e => e.Class == "humidity"), humi },
                    { SensorsList.First(e => e.Class == "illuminance"), illu },
                    { SensorsList.First(e => e.Class == "battery"), batt }
                };

                // add values into json
                foreach (var sensor in SensorsList)
                    payloadObj.Add(sensor.Value, JToken.FromObject(vals[sensor]));

                // delete serviceData key, to avoid re-processing of the message
                payloadObj.Remove("servicedata");

                payload = payloadObj.ToString();

                // send message
                await client.PublishAsync(e.ApplicationMessage.Topic, payload, MqttConfiguration.MqttQosLevel);

                Logger.LogInformation("Worker send message: {0}", payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Worker error");
            }
        }

        private async Task SubscribeToAllDevices(IManagedMqttClient client)
        {
            foreach (var device in ProgramConfiguration.AprilBeaconDevicesList)
                await client.SubscribeAsync($"{MqttConfiguration.TopicBase}/{device}", MqttConfiguration.MqttQosLevel);
        }

        /// <summary>
        /// send sensor configuration message to HomeAssistant
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private async Task SendSensorConfiguration(IManagedMqttClient client)
        {
            foreach (var device in ProgramConfiguration.AprilBeaconDevicesList)
                foreach (var sensor in SensorsList)
                {
                    var name = string.Format(sensor.NamePattern, device);
                    var uniqueId = string.Format(sensor.UniqueIdPattern, device);

                    var configurationPayload = $"{{\"stat_t\":\"{MqttConfiguration.TopicBase}/{device}\"," +
                        $"\"name\":\"{name}\",\"uniq_id\":\"{uniqueId}\",\"dev_cla\":\"{sensor.Class}\"," +
                        $"\"val_tpl\":\"{{{{ value_json.{sensor.Value} | is_defined }}}}\",\"unit_of_meas\":\"{sensor.Unit}\"}}";

                    var configurationTopic = $"homeassistant/sensor/{uniqueId}/config";

                    await client.PublishAsync(configurationTopic, configurationPayload, MqttConfiguration.MqttQosLevel);
                }
        }
    }
}
