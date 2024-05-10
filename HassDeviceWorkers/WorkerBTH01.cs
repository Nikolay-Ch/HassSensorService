using HassDeviceBaseWorkers;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BTHomePacketDecoder;

namespace HassDeviceWorkers
{
    /// <summary>
    /// Convert advertising raw-data into normal values for HomeAssistant MQTT (with autodiscover option)
    /// see documentation of the BTH01 custom firmware
    /// https://github.com/pvvx/THB2
    /// https://pvvx.github.io/BTH01/
    /// </summary>
    public class WorkerBTH01 : DeviceBaseWorker<WorkerBTH01>
    {
        public WorkerBTH01(string deviceId, ILogger<WorkerBTH01> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, logger, workersConfiguration, mqttConfiguration, mqttClient)
        {
            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = $"PVVX-{DeviceId[6..]}",
                Model = "BTH01",
                Manufacturer = "TUYA",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = [DeviceId],
                Connections = [["mac", DeviceId]]
            };

            ComponentList.AddRange(new List<IHassComponent>
            {
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.Temperature, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.Humidity, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.Battery, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.Voltage, Device = device }),
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
                if (payloadObj == null)
                {
                    Logger.LogTrace("{className} - {funcName} - payloadObj is null}",
                        GetType().Name, nameof(MqttReceiveHandler));
                    return;
                }

                if (!payloadObj.TryGetPropertyValue("servicedatauuid", out var serviceDataUuidObj)
                    || serviceDataUuidObj.Deserialize<string>() != "0xfcd2")
                {
                    Logger.LogTrace("{className} - {funcName} - servicedatauuid={serviceDataUuid} not is '0xfcd2'",
                        GetType().Name, nameof(MqttReceiveHandler), serviceDataUuidObj.Deserialize<string>());

                    return;
                }

                if (!payloadObj.TryGetPropertyValue("servicedata", out var serviceDataObj))
                {
                    Logger.LogTrace("{className} - {funcName} - servicedata is empty",
                        GetType().Name, nameof(MqttReceiveHandler));

                    return;
                }

                var serviceData = Convert.FromHexString(serviceDataObj!.ToString()) ?? [];

                var packet = new BTHomeV2Packet(serviceData);

                // add values into dictionary to easy search by sensor-type
                var vals = new Dictionary<IHassComponent, object>
                {
                    { ComponentList.First(e => e.DeviceClass == "temperature"), packet[BTHomeObjectId.Temperature] },
                    { ComponentList.First(e => e.DeviceClass == "humidity"), packet[BTHomeObjectId.Humidity] },
                    { ComponentList.First(e => e.DeviceClass == "battery"), packet[BTHomeObjectId.Battery] },
                    { ComponentList.First(e => e.DeviceClass == "voltage"), packet[BTHomeObjectId.Voltage] }
                };

                // add values into json
                foreach (var sensor in ComponentList)
                    payloadObj.Add(sensor.DeviceClassDescription.ValueName, JsonSerializer.Serialize(vals[sensor]));

                // delete serviceData key, to avoid re-processing of the message
                payloadObj.Remove("servicedata");

                // send message
                await SendDeviceInformation(ComponentList[0], payloadObj);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }
    }
}
