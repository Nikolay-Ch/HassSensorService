using HassDeviceBaseWorkers;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        public WorkerABN03(
            IReadOnlyDictionary<string, string> workerParameters,
            IMemoryCache cache,
            ILogger<WorkerABN03> logger,
            IOptions<WorkersConfiguration> workersConfiguration,
            IOptions<MqttConfiguration> mqttConfiguration,
            IMqttClientForMultipleSubscribers mqttClient)
            : base(cache, workerParameters, logger, workersConfiguration, mqttConfiguration, mqttClient)
        {
            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = $"ABN03-{DeviceId[6..]}",
                Model = "ABSensor N03",
                Manufacturer = "April Brother",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = [DeviceId],
                Connections = [["mac", DeviceId]]
            };

            ComponentList.AddRange(
            [
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.TemperatureCelsius, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.Humidity, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.IlluminanceLux, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.Battery, Device = device })
            ]);
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
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
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
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                var payloadObj = JsonSerializer.Deserialize<JsonObject>(payload);

                if (payloadObj == null || !payloadObj.TryGetPropertyValue("servicedata", out var serviceDataObj))
                    return;

                var serviceData = serviceDataObj.Deserialize<string>() ?? "";

                // if serviceData not started with ab03, that we can't parse data - skip parsing
                if (!serviceData.StartsWith("ab03"))
                    return;

                // convert hexadecimal string info byte-array
                var payloadBytes = serviceData.FromHexStringToByteArray();

                // delete serviceData key, to avoid re-processing of the message
                payloadObj.Remove("servicedata");

                // create parsed messages
                var temp = payloadBytes.ParseShortLE(9, 8);
                var humi = payloadBytes.ParseShortLE(11, 2);
                var illu = payloadBytes.ParseTwoBytesLE(13);
                var batt = payloadBytes[8];

                var jsonPayload = CreatePayloadObject(payloadObj);
                // add values into json (or ignore it if in cache and equals to cache)
                jsonPayload.CachedAdd(GetValueName("tempc"), temp);
                jsonPayload.CachedAdd(GetValueName("hum"), humi);
                jsonPayload.CachedAdd(GetValueName("lux"), illu);
                jsonPayload.CachedAdd(GetValueName("batt"), batt);

                // send message
                await SendDeviceInformation(ComponentList[0].StateTopic, jsonPayload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }
    }
}
