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
    public class Zm1940d9yWorker : ModbusWorker<Sdm120mWorker>
    {
        public Zm1940d9yWorker(string deviceId, ILogger<Sdm120mWorker> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, logger, workersConfiguration, mqttConfiguration, modbusGatewayConfiguration, mqttClient)
        {
            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = "ZM194-D9Y",
                Model = "3-Phase Power Meter ZM194-D9Y",
                Manufacturer = "ZM",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = new() { new() { "mac", DeviceId } }
            };

            ComponentList.AddRange(new List<IHassComponent>
            {
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "ph1_volt" }, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "ph2_volt" }, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "ph3_volt" }, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "ln12_volt" }, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "ln23_volt" }, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "ln31_volt" }, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = DeviceClassDescription.FrequencyHz, Device = device }),
                sensorFactory.CreateComponent(new AnalogSensorDescription { DeviceClassDescription = new(DeviceClassDescription.Voltage) { ValueName = "unb_volt" }, Device = device })
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

                var register = new Register(1, 3, "Raw", 0x36, "raw units", RegisterFormat.Hex, 0x00);
                mrr.ReadRegister(register);

                var zm194 = new Zm1940d9y(register.RawValue);

                payload.Add(new JProperty(GetValueName("ph1_volt"), zm194.Phase1Voltage));
                payload.Add(new JProperty(GetValueName("ph2_volt"), zm194.Phase2Voltage));
                payload.Add(new JProperty(GetValueName("ph3_volt"), zm194.Phase3Voltage));
                payload.Add(new JProperty(GetValueName("ln12_volt"), zm194.Line1To2Voltage));
                payload.Add(new JProperty(GetValueName("ln23_volt"), zm194.Line2To3Voltage));
                payload.Add(new JProperty(GetValueName("ln31_volt"), zm194.Line3To1Voltage));
                payload.Add(new JProperty(GetValueName("freqh"), zm194.Frequency));
                payload.Add(new JProperty(GetValueName("unb_volt"), zm194.VoltageUnbalance));

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
