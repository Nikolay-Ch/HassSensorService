using HassDeviceBaseWorkers;
using HassDeviceWorkers.ModBus;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HassDeviceWorkers
{
    public class Zm1940d9yWorker : ModbusWorker<Sdm120mWorker>
    {
        public Zm1940d9yWorker(
            IReadOnlyDictionary<string, string> workerParameters,
            IMemoryCache cache,
            ILoggerFactory loggerFactory,
            IOptions<WorkersConfiguration> workersConfiguration,
            IOptions<MqttConfiguration> mqttConfiguration,
            IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration,
            IMqttClientForMultipleSubscribers mqttClient)
            : base(cache, workerParameters, loggerFactory, workersConfiguration, mqttConfiguration, modbusGatewayConfiguration, mqttClient)
        {
            var sensorFactory = new AnalogSensorFactory();
            var device = new Device
            {
                Name = $"ZM194-D9Y-{DeviceId[6..]}",
                Model = "3-Phase Power Meter",
                Manufacturer = "ZM",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = [DeviceId],
                Connections = [["mac", DeviceId]]
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

                var register = new Register(3, 3, "Raw", 0x36, "raw units", RegisterFormat.Hex, 0x00);
                await mrr.ReadRegister(register);

                if (register.RawValue.Length == 0)
                {
                    Logger.LogInformation("Receiving empty data");
                    return;
                }

                var zm194 = new Zm1940d9y(register.RawValue);

                payload.CachedAdd(GetValueName("ph1_volt"), zm194.Phase1Voltage);
                payload.CachedAdd(GetValueName("ph2_volt"), zm194.Phase2Voltage);
                payload.CachedAdd(GetValueName("ph3_volt"), zm194.Phase3Voltage);
                payload.CachedAdd(GetValueName("ln12_volt"), zm194.Line1To2Voltage);
                payload.CachedAdd(GetValueName("ln23_volt"), zm194.Line2To3Voltage);
                payload.CachedAdd(GetValueName("ln31_volt"), zm194.Line3To1Voltage);
                payload.CachedAdd(GetValueName("freqh"), zm194.Frequency);
                payload.CachedAdd(GetValueName("unb_volt"), zm194.VoltageUnbalance);

                // send message
                await SendDeviceInformation(ComponentList[0].StateTopic, payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }
    }
}
