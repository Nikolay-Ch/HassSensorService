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
        public RsGzwsN01Worker(string deviceId, ILoggerFactory loggerFactory, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, loggerFactory, workersConfiguration, mqttConfiguration, modbusGatewayConfiguration, mqttClient)
        {
            //ModbusGatewayConfiguration.SendTimeout = ModbusGatewayConfiguration < 60000

            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = "RS-GZWS-N01",
                Model = "Light intensity with temperature and humidity sensor",
                Manufacturer = "Wangzi",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = [DeviceId],
                Connections = [new() { "mac", DeviceId }]
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
            Logger.LogTrace("SendWorkerHeartBeat: Method starts... {type}", GetType().Name);

            string GetValueName(string valueName) => ComponentList
                                                        .Single(e => e.DeviceClassDescription.ValueName == valueName)
                                                        .DeviceClassDescription
                                                        .ValueName;

            try
            {
                var payload = CreatePayloadObject();

                var register = new Register(4, 3, "Raw", 0x07, "raw units", RegisterFormat.Hex, 0x00);
                var bytesReaded = await mrr.ReadRegister(register);

                Logger.LogTrace("SendWorkerHeartBeat: Bytes readed from Modbus: {bytesReaded}", bytesReaded);

                if (bytesReaded == 0)
                {
                    Logger.LogInformation("Receiving empty data");
                    return;
                }

                var zm194 = new RsGzwsN01(register.RawValue);

                payload.Add(new JProperty(GetValueName("tempc"), zm194.Temperature));
                payload.Add(new JProperty(GetValueName("hum"), zm194.Humidity));
                payload.Add(new JProperty(GetValueName("lux"), zm194.LightIntensity));

                // send message
                await SendDeviceInformation(ComponentList[0], payload);

                Logger.LogTrace("SendWorkerHeartBeat: Method ends... {type}", GetType().Name);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }
    }
}