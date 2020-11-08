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

        protected override List<Sensor> SensorsList { get; } = new List<Sensor>
        {
            new Sensor { Name = "ABN03_tem", UniqueId = "{0}-ABN03-tem", Class = "temperature", Category = SensorCategory.sensor, ValueName = "temp", Unit = "C" },
            new Sensor { Name = "ABN03_hum", UniqueId = "{0}-ABN03-hum", Class = "humidity", Category = SensorCategory.sensor, ValueName = "hum", Unit = "%" },
            new Sensor { Name = "ABN03_lux", UniqueId = "{0}-ABN03-lux", Class = "illuminance", Category = SensorCategory.sensor, ValueName = "lux", Unit = "lx" },
            new Sensor { Name = "ABN03_batt", UniqueId = "{0}-ABN03-batt", Class = "battery", Category = SensorCategory.sensor, ValueName = "batt", Unit = "%" },
        };

        public WorkerABN03(ILogger<WorkerABN03> logger, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ProgramConfiguration> programConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(logger, mqttConfiguration, programConfiguration, mqttClient) { }

        protected override async Task PostExecuteAsync(CancellationToken stoppingToken)
        {
            // subscribe to all devices raw-messages
            foreach (var device in ProgramConfiguration.AprilBeaconDevicesList)
                await MqttClient.SubscribeAsync(this, $"{MqttConfiguration.TopicBase}/{device}", MqttConfiguration.MqttQosLevel);
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
                    { SensorsList.First(e => e.Class == "temperature"), temp },
                    { SensorsList.First(e => e.Class == "humidity"), humi },
                    { SensorsList.First(e => e.Class == "illuminance"), illu },
                    { SensorsList.First(e => e.Class == "battery"), batt }
                };

                // add values into json
                foreach (var sensor in SensorsList)
                    payloadObj.Add(sensor.ValueName, JToken.FromObject(vals[sensor]));

                // delete serviceData key, to avoid re-processing of the message
                payloadObj.Remove("servicedata");

                payload = payloadObj.ToString();

                // send message
                await MqttClient.PublishAsync(e.ApplicationMessage.Topic, payload, MqttConfiguration.MqttQosLevel);

                Logger.LogInformation("WorkerABN03 send message: {0}", payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "WorkerABN03 error");
            }
        }
    }
}
