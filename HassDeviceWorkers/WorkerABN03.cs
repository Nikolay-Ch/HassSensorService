using HassDeviceBaseWorkers;
using HassMqttIntegration;
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

namespace HassDeviceWorkers
{
    /// <summary>
    /// Convert advertising raw-data into normal values for HomeAssistant MQTT (with autodiscover option)
    /// see documentation of the N03 April Beacon device
    /// https://wiki.aprbrother.com/en/ABSensor.html#absensor-n03
    /// </summary>
    public class WorkerABN03 : DeviceBaseWorker<WorkerABN03>
    {
        public WorkerABN03(string deviceId, ILogger<WorkerABN03> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, logger, workersConfiguration, mqttConfiguration, mqttClient)
        {
            var sensorFactory = new SensorFactory();
            var device = new Device
            {
                Name = "ABN03",
                Model = "ABSensor N03",
                Manufacturer = "April Brother",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = new() { new() { "mac", DeviceId } }
            };

            ComponentList.AddRange(new List<Sensor>
            {
                sensorFactory.CreateSensor(DeviceClassDescription.Temperature, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.Humidity, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.IlluminanceLux, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.Battery, device: device)
            });
        }

        protected override async Task PostSendConfigurationAsync(CancellationToken stoppingToken)
        {
            try
            {
                // subscribe to all devices raw-messages
                await MqttClient.SubscribeAsync(this, $"{MqttConfiguration.TopicToSubscribe}/{DeviceId}", MqttConfiguration.MqttQosLevel);
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
                var vals = new Dictionary<IHassComponent, object>
                {
                    { ComponentList.First(e => e.DeviceClass == "temperature"), temp },
                    { ComponentList.First(e => e.DeviceClass == "humidity"), humi },
                    { ComponentList.First(e => e.DeviceClass == "illuminance"), illu },
                    { ComponentList.First(e => e.DeviceClass == "battery"), batt }
                };

                // add values into json
                foreach (var sensor in ComponentList)
                    payloadObj.Add(sensor.DeviceClassDescription.ValueName, JToken.FromObject(vals[sensor]));

                // delete serviceData key, to avoid re-processing of the message
                payloadObj.Remove("servicedata");

                // send message
                await SendDeviceInformation(ComponentList[0], payloadObj);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{GetType().Name} error at {DateTimeOffset.Now}");
            }
        }
    }
}
