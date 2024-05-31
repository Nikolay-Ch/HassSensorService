using HassDeviceBaseWorkers;
using HassDeviceWorkers.HPiLO4DataReader;
using HassDeviceWorkers.HPiLO4DataReader.DTO;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceWorkers
{
    /// <summary>
    /// Send HP Gen8 iLO4 - metrics to HomeAssistant
    /// </summary>
    public partial class HPiLO4DataWorker : DeviceBaseWorker<HPiLO4DataWorker>
    {
        public int SendTimeout { get; set; } = 30000;
        public ILO4DataReader ILO4DataReader { get; }

        protected override string DeviceId { get; }

        public HPiLO4DataWorker(
            IReadOnlyDictionary<string, string> workerParameters,
            IMemoryCache cache,
            ILogger<HPiLO4DataWorker> logger,
            IOptions<WorkersConfiguration> workersConfiguration,
            IOptions<MqttConfiguration> mqttConfiguration,
            IMqttClientForMultipleSubscribers mqttClient)
            : base(cache, workerParameters, logger, workersConfiguration, mqttConfiguration, mqttClient)
        {
            ILO4DataReader = new ILO4DataReader(
                workerParameters["host"],
                workerParameters["user"],
                workerParameters["pass"]);

            var hostData = ILO4DataReader.GetHostData().Result;
            var healthData = ILO4DataReader.GetHealthAndUptime().Result;

            var sensorFactory = new AnalogSensorFactory();
            var device = new Device()
            {
                Name = "HP iLO",
                Manufacturer = "Hewlett Packard",
                Model = hostData.ProductName ?? "",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = [hostData.SerialNumber ?? throw new NullReferenceException("HP server serial number is null!")],
                Connections = hostData.NetworkCards
                    .Where(e => e.Mac != null)
                    .Select(e => new List<string> { "mac", e.Mac! }).ToList()
            };

            DeviceId = hostData.SerialNumber;

            ComponentList.AddRange(
            [
                sensorFactory.CreateEnumComponent(device, "HealthBiosHardware", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthFans", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthTemperature", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthPowerSupplies", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthProcessor", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthMemory", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthNetwork", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
                sensorFactory.CreateEnumComponent(device, "HealthStorage", ["Undefined", "OK", "Warning", "Critical"], "mdi:list-status"),
            ]);

            foreach (var temperature in healthData.Temperatures)
            {
                // temperature sensor
                var sensorTemp = sensorFactory.CreateComponent(
                    new AnalogSensorDescription
                    {
                        DeviceClassDescription = DeviceClassDescription.TemperatureCelsiusWithName(ClearSensorName(temperature.Label, "temp")),
                        Device = device,
                        SensorName = temperature.Label,
                        HasAttributes = true,
                    });

                ComponentList.Add(sensorTemp);
            }

            foreach (var powerSupply in healthData.PowerSupplies)
            {
                // power supply sensor
                var powerSupplyTemp = sensorFactory.CreateComponent(
                    new AnalogSensorDescription
                    {
                        StateClass = StateClass.None,
                        DeviceClassDescription = DeviceClassDescription.NoneWithName(ClearSensorName(powerSupply.Label, "power")),
                        Device = device,
                        SensorIcon = "mdi:lightning-bolt-circle",
                        SensorName = powerSupply.Label
                    });

                ComponentList.Add(powerSupplyTemp);
            }

            foreach (var fan in healthData.Fans)
            {
                // fan sensor
                var sensorFan = sensorFactory.CreateComponent(
                    new AnalogSensorDescription
                    {
                        DeviceClassDescription = new DeviceClassDescription
                        {
                            UnitOfMeasures = "%",
                            ValueName = ClearSensorName(fan.Label, "fan")
                        },
                        Device = device,
                        SensorName = fan.Label,
                        SensorIcon = "mdi:fan",
                        HasAttributes = true,
                    });

                ComponentList.Add(sensorFan);
            }

            // server uptime sensor
            var sensorUptime = sensorFactory.CreateComponent(
                new AnalogSensorDescription
                {
                    DeviceClassDescription = new DeviceClassDescription
                    {
                        DeviceClass = "timestamp",
                        ValueName = "Uptime",
                    },
                    Device = device,
                    SensorName = "Uptime",
                    SensorIcon = "mdi:clock",
                    EntityCategory = "diagnostic",
                    StateClass = StateClass.None,
                });

            ComponentList.Add(sensorUptime);
        }

        protected override async Task PostSendConfigurationAsync(CancellationToken stoppingToken)
        {
            WorkingTasks.Add(Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await SendWorkerHeartBeat();
                    await Task.Delay(SendTimeout, stoppingToken);
                }
            }, stoppingToken));

            await Task.CompletedTask;
        }

        private async Task SendWorkerHeartBeat()
         {
            try
            {
                var attributes = new Dictionary<string, JsonCachedPayload>();

                var healthData = ILO4DataReader.GetHealthAndUptime().Result;

                var payload = CreatePayloadObject();

                AddHealthPayload(healthData.HealthAtAGlance, payload);
                AddTemperaturePayload(healthData.Temperatures, payload, attributes);
                AddPowerSupplyPayload(healthData.PowerSupplies, payload, attributes);
                AddFansPayload(healthData.Fans, payload, attributes);
                AddServerUptimePayload(healthData.ServerUptime , payload);

                // send message for state
                await SendDeviceInformation(ComponentList[0].StateTopic, payload);

                // send messages for attributes
                foreach (var attributesSensor in attributes)
                    await SendDeviceInformation(attributesSensor.Key, attributesSensor.Value);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }
        }

        private static void AddHealthPayload(HealthAtAGlance healthAtAGlance, JsonCachedPayload payload)
        {
            payload.CachedAdd("HealthBiosHardware", healthAtAGlance.BiosHardware.ToString("G"));
            payload.CachedAdd("HealthFans", healthAtAGlance.Fans.ToString("G"));
            payload.CachedAdd("HealthTemperature", healthAtAGlance.Temperature.ToString("G"));
            payload.CachedAdd("HealthPowerSupplies", healthAtAGlance.PowerSupplies.ToString("G"));
            payload.CachedAdd("HealthProcessor", healthAtAGlance.Processor.ToString("G"));
            payload.CachedAdd("HealthMemory", healthAtAGlance.Memory.ToString("G"));
            payload.CachedAdd("HealthNetwork", healthAtAGlance.Network.ToString("G"));
            payload.CachedAdd("HealthStorage", healthAtAGlance.Storage.ToString("G"));
        }

        private void AddTemperaturePayload(IEnumerable<Temperature> temperatures, JsonCachedPayload payload, Dictionary<string, JsonCachedPayload> attributes)
        {
            foreach (var temperature in temperatures)
            {
                var temperatureUniqueId = ClearSensorName(temperature.Label, "temp");
                if (temperatureUniqueId == null)
                    continue;

                var componentId = ComponentList.First(e => e.Name == temperature.Label);
                if (componentId == null)
                    continue;

                payload.CachedAdd(temperatureUniqueId, temperature.CurrentTemperature);

                var attributesJson = CreatePayloadObject([]);
                attributesJson.CachedAdd("CautionTemperature", temperature.CautionTemperature);
                attributesJson.CachedAdd("CriticalTemperature", temperature.CriticalTemperature);
                attributesJson.CachedAdd("Status", temperature.Status.ToString("G"));

                attributes.Add(componentId.AttributesTopic!, attributesJson);
            }
        }

        private void AddPowerSupplyPayload(IEnumerable<PowerSupply> powerSupplies, JsonCachedPayload payload, Dictionary<string, JsonCachedPayload> attributes)
        {
            foreach (var powerSupply in powerSupplies)
            {
                var powerSupplyUniqueId = ClearSensorName(powerSupply.Label, "power");
                if (powerSupplyUniqueId == null)
                    continue;

                var componentId = ComponentList.First(e => e.Name == powerSupply.Label);
                if (componentId == null)
                    continue;

                payload.CachedAdd(powerSupplyUniqueId, powerSupply.Status);
            }
        }

        private void AddFansPayload(IEnumerable<Fan> fans, JsonCachedPayload payload, Dictionary<string, JsonCachedPayload> attributes)
        {
            foreach(var fan in fans)
            {
                var fanUniqueId = ClearSensorName(fan.Label, "fan");
                if (fanUniqueId == null)
                    continue;

                var componentId = ComponentList.First(e=>e.Name == fan.Label);
                if(componentId == null)
                    continue;

                payload.CachedAdd(fanUniqueId, fan.SpeedInPercent);

                var attributesJson = CreatePayloadObject([]);
                attributesJson.CachedAdd("Zone", fan.Zone);
                attributesJson.CachedAdd("Status", fan.Status.ToString("G"));

                attributes.Add(componentId.AttributesTopic!, attributesJson);
            }
        }

        private void AddServerUptimePayload(DateTime serverUptime, JsonCachedPayload payload)
        {
            payload.CachedAdd("Uptime", serverUptime);
        }

        [GeneratedRegex(@"\d+$")]
        private static partial Regex IdRegex();

        private static string ClearSensorName(string sensorName, string sensorPostfix) =>
            $"_{sensorName.Replace(" ", "").Replace("-", "")}_{sensorPostfix}";
    }
}
