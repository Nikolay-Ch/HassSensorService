using HassDeviceBaseWorkers;
using HassDeviceWorkers.ModBus;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HassDeviceWorkers
{
    public class RsGzwsN01Worker : ModbusWorker<RsGzwsN01Worker>
    {
        public RsGzwsN01Worker(string deviceId, ILogger<RsGzwsN01Worker> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, logger, workersConfiguration, mqttConfiguration, modbusGatewayConfiguration, mqttClient)
        {
            //ModbusGatewayConfiguration.SendTimeout = ModbusGatewayConfiguration < 60000

            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = "RS-GZWS-N01",
                Model = "Light intensity with temperature and humidity sensor",
                Manufacturer = "Wangzi",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = new() { new() { "mac", DeviceId } }
            };

            ComponentList.AddRange(new List<IHassComponent>
            {
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.TemperatureCelsius, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.Humidity, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.IlluminanceLux, Device = device }),
            });
        }

        protected override async Task SendWorkerHeartBeat(ModbusRegisterReader mrr)
        {
            string GetValueName(string valueName) => ComponentList
                                                        .Single(e => e.DeviceClassDescription.ValueName == valueName)
                                                        .DeviceClassDescription
                                                        .ValueName;

            try
            {
                var payload = CreatePayloadObject();

                var register = new Register(4, 3, "Raw", 0x07, "raw units", RegisterFormat.Hex, 0x00);
                await mrr.ReadRegister(register);

                var zm194 = new RsGzwsN01(register.RawValue);

                payload.Add(new JProperty(GetValueName("tempc"), zm194.Temperature));
                payload.Add(new JProperty(GetValueName("hum"), zm194.Humidity));
                payload.Add(new JProperty(GetValueName("lux"), zm194.LightIntensity));

                // send message
                await SendDeviceInformation(ComponentList[0], payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }
    }
}