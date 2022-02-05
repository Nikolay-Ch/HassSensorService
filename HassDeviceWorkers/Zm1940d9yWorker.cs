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
            var sensorFactory = new SensorFactory();
            var device = new Device
            {
                Name = "ZM194-D9Y",
                Model = "3-Phase Power Meter ZM194-D9Y",
                Manufacturer = "ZM",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = new() { new() { "mac", DeviceId } }
            };

            ComponentList.AddRange(new List<Sensor>
            {
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "PhaseV1" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "PhaseV2" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "PhaseV3" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "LineV1" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "LineV2" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "LineV3" }, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.Frequency, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Voltage) { ValueName = "VUnbalance" }, device: device),
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

                payload.Add(new JProperty(GetValueName("PhaseV1"), zm194.PhaseVoltage1));
                payload.Add(new JProperty(GetValueName("PhaseV2"), zm194.PhaseVoltage2));
                payload.Add(new JProperty(GetValueName("PhaseV3"), zm194.PhaseVoltage3));
                payload.Add(new JProperty(GetValueName("LineV1"), zm194.LineVoltage1));
                payload.Add(new JProperty(GetValueName("LineV2"), zm194.LineVoltage2));
                payload.Add(new JProperty(GetValueName("LineV3"), zm194.LineVoltage3));
                payload.Add(new JProperty(GetValueName("freq"), zm194.Frequency));
                payload.Add(new JProperty(GetValueName("VUnbalance"), zm194.VoltageUnbalance));

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
