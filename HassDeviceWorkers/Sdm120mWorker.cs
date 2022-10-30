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
    public class Sdm120mWorker : ModbusWorker<Sdm120mWorker>
    {
        public Sdm120mWorker(string deviceId, ILoggerFactory loggerFactory, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, loggerFactory, workersConfiguration, mqttConfiguration, modbusGatewayConfiguration, mqttClient)
        {
            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = "SDM120M",
                Model = "Power Meter SDM120M",
                Manufacturer = "Eastron",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = new() { new() { "mac", DeviceId } }
            };

            ComponentList.AddRange(new List<IHassComponent>
            {
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.Voltage, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.Current, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.Power, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.ReactivePower, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.ApparentPower, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.PowerFactor, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription {DeviceClassDescription = DeviceClassDescription.FrequencyHz, Device = device }),
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

                var register = new Register(2, 4, "Raw", 0x48, "raw units", RegisterFormat.Hex, 0x00);
                await mrr.ReadRegister(register);

                if (register.RawValue.Length == 0)
                {
                    Logger.LogInformation("Receiving empty data");
                    return;
                }

                var sdm120 = new Sdm120(register.RawValue);

                payload.Add(new JProperty(GetValueName("volt"), sdm120.Voltage));
                payload.Add(new JProperty(GetValueName("amps"), sdm120.Current));
                payload.Add(new JProperty(GetValueName("watt"), sdm120.ActivePower));
                payload.Add(new JProperty(GetValueName("var"), sdm120.ReactivePower));
                payload.Add(new JProperty(GetValueName("va"), sdm120.ApparentPower));
                payload.Add(new JProperty(GetValueName("pfact"), sdm120.PowerFactor));
                payload.Add(new JProperty(GetValueName("freqh"), sdm120.Frequency));

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
