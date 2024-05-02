﻿using HassDeviceBaseWorkers;
using HassDeviceWorkers.ModBus;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
                Identifiers = [DeviceId],
                Connections = [["mac", DeviceId]]
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

                byte[] bytesReaded = new byte[0x0160 * 2];

                if (!await ReadFromModBus(mrr, 0x60, 0x00, bytesReaded))
                    return;

                if (!await ReadFromModBus(mrr, 0x5e, 0x0102, bytesReaded))
                    return;

                var sdm120 = new Sdm120(bytesReaded);

                payload.Add(GetValueName("volt"), sdm120.Voltage);
                payload.Add(GetValueName("amps"), sdm120.Current);
                payload.Add(GetValueName("watt"), sdm120.ActivePower);
                payload.Add(GetValueName("var"), sdm120.ReactivePower);
                payload.Add(GetValueName("va"), sdm120.ApparentPower);
                payload.Add(GetValueName("pfact"), sdm120.PowerFactor);
                payload.Add(GetValueName("freqh"), sdm120.Frequency);
                payload.Add(GetValueName("ekwh"), sdm120.TotalActiveEnergy);

                // send message
                await SendDeviceInformation(ComponentList[0], payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }

        private async Task<bool> ReadFromModBus(ModbusRegisterReader reader, int length, int address, byte[] buffer)
        {
            Register register = new(2, 4, "Raw", length, "raw units", RegisterFormat.Hex, address);
            await reader.ReadRegister(register);
            if (register.RawValue.Length == 0)
            {
                Logger.LogInformation("Receiving empty data");
                return false;
            }

            register.RawValue.CopyTo(buffer, address * 2);

            return true;
        }
    }
}
