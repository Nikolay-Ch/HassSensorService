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
        public Sdm120mWorker(string deviceId, ILogger<Sdm120mWorker> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, logger, workersConfiguration, mqttConfiguration, modbusGatewayConfiguration, mqttClient)
        {
            var sensorFactory = new SensorFactory();
            var device = new Device
            {
                Name = "SDM120M",
                Model = "Power Meter SDM120M",
                Manufacturer = "Eastron",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = new() { new() { "mac", DeviceId } }
            };

            ComponentList.AddRange(new List<Sensor>
            {
                sensorFactory.CreateSensor(DeviceClassDescription.Voltage, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.Current, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Power) { ValueName = "acwatt" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Power) { ValueName = "rewatt" }, device: device),
                sensorFactory.CreateSensor(new(DeviceClassDescription.Power) { ValueName = "apwatt" }, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.PowerFactor, device: device),
                sensorFactory.CreateSensor(DeviceClassDescription.Frequency, device: device),
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
                mrr.ReadRegister(register);

                var sdm120 = new Sdm120(register.RawValue);

                payload.Add(new JProperty(GetValueName("volt"), sdm120.Voltage));
                payload.Add(new JProperty(GetValueName("amps"), sdm120.Current));
                payload.Add(new JProperty(GetValueName("acwatt"), sdm120.ActivePower));
                payload.Add(new JProperty(GetValueName("rewatt"), sdm120.ReactivePower));
                payload.Add(new JProperty(GetValueName("apwatt"), sdm120.ApparentPower));
                payload.Add(new JProperty(GetValueName("pfact"), sdm120.PowerFactor));
                payload.Add(new JProperty(GetValueName("freq"), sdm120.Frequency));

                // send message
                await SendDeviceInformation(ComponentList[0], payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{GetType().Name} error at {DateTimeOffset.Now}");
            }
        }
    }
}
