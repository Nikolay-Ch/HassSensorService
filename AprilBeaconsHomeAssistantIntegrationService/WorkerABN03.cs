using HassSensorConfiguration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
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
    public class WorkerABN03 : WorkerWithSensorsBase<WorkerABN03>
    {
        protected override List<string> Devices => ProgramConfiguration.AprilBeaconDevicesList;

        protected override List<Sensor> SensorsList { get; }

        public WorkerABN03(ILogger<WorkerABN03> logger, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ProgramConfiguration> programConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(logger, mqttConfiguration, programConfiguration, mqttClient)
        {
            SensorsList = new List<Sensor>
            {
                new Sensor { SensorClass = SensorClass.temperature,
                    Device = new Device{ Name = "ABN03", Model = "ABSensor N03", Manufacturer = "April Brother", ViaDevice = ProgramConfiguration.ServiceName } },

                new Sensor { SensorClass = SensorClass.humidity,
                    Device = new Device{ Name = "ABN03", Model = "ABSensor N03", Manufacturer = "April Brother", ViaDevice = ProgramConfiguration.ServiceName } },

                new Sensor { SensorClass = SensorClass.illuminance,
                    Device = new Device{ Name = "ABN03", Model = "ABSensor N03", Manufacturer = "April Brother", ViaDevice = ProgramConfiguration.ServiceName } },

                new Sensor { SensorClass = SensorClass.battery,
                    Device = new Device{ Name = "ABN03", Model = "ABSensor N03", Manufacturer = "April Brother", ViaDevice = ProgramConfiguration.ServiceName } }
            };
        }

        protected override async Task PostExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                // subscribe to all devices raw-messages
                foreach (var device in ProgramConfiguration.AprilBeaconDevicesList)
                    await MqttClient.SubscribeAsync(this, $"{MqttConfiguration.TopicToSubscribe}/{device}", MqttConfiguration.MqttQosLevel);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"WorkerABN03 error at {DateTimeOffset.Now}");
            }
        }

        /// <summary>
        /// Try to convert sensor raw-data to parsed data used in HomeAssistant
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override async Task MqttReceiveHandler(MqttApplicationMessageReceivedEventArgs e)
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
                    { SensorsList.First(e => e.SensorClass == SensorClass.temperature), temp },
                    { SensorsList.First(e => e.SensorClass == SensorClass.humidity), humi },
                    { SensorsList.First(e => e.SensorClass == SensorClass.illuminance), illu },
                    { SensorsList.First(e => e.SensorClass == SensorClass.battery), batt }
                };

                // add values into json
                foreach (var sensor in SensorsList)
                    payloadObj.Add(sensor.SensorClass.ValueName(), JToken.FromObject(vals[sensor]));

                // delete serviceData key, to avoid re-processing of the message
                payloadObj.Remove("servicedata");

                payload = payloadObj.ToString();

                // send message
                await MqttClient.PublishAsync(
                    $"{string.Format(MqttConfiguration.TopicBase, ProgramConfiguration.ServiceName)}/{payloadObj.Value<string>("id").Replace(":", "")}",
                    payload, MqttConfiguration.MqttQosLevel, false);

                Logger.LogInformation($"WorkerABN03 send message: {payload} at {DateTimeOffset.Now}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"WorkerABN03 error at {DateTimeOffset.Now}");
            }
        }
    }
}
