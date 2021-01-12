using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public class ThermalZone
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Name => $"OS_TEMP_{Type}{Id}"; // for identification purposes...
    }

    /// <summary>
    /// Send Unix-metrict to HomeAssistant
    /// </summary>
    class WorkerLinuxSensors : WorkerWithSensorsBase<WorkerLinuxSensors>
    {
        public int SendTimeout { get; set; } = 10000;

        protected override List<string> Devices => new List<string>
        {
            new DeviceIdBuilder()
                //.AddOSInstallationID()
                .AddProcessorId()
                .UseFormatter(new StringDeviceIdFormatter(new PlainTextDeviceIdComponentEncoder()))
                .ToString()
        };

        protected static string ThermalZonesPath => "/sys/class";

        // CPU utilization (read data from /proc/stat https://rosettacode.org/wiki/Linux_CPU_utilization)
        // Memory Usage (read data from /proc/meminfo https://vitux.com/5-ways-to-check-available-memory-in-ubuntu/)
        // Disk usage
        // TemperatureХХ (scans for all thermal_zone sensors in /sys/class/thermal/thermal_zoneХХ https://askubuntu.com/questions/229929/correct-sensor-location-to-use-for-temperature-monitor-in-lxpanel)
        protected override List<Sensor> SensorsList { get; } = new List<Sensor>
        {
            //new Sensor { Name = "OS_CPU_load", UniqueId = "{0}-OS-cpu-load", Class = "temperature", Category = SensorCategory.sensor, ValueName = "temp", Unit = "C" },
            //new Sensor { Name = "OS_MEM_usage", UniqueId = "{0}-OS-mem-usage", Class = "humidity", Category = SensorCategory.sensor, ValueName = "hum", Unit = "%" },
            //new Sensor { Name = "OS_DSK_usage", UniqueId = "{0}-OS-disk-usage", Class = "illuminance", Category = SensorCategory.sensor, ValueName = "lux", Unit = "lx" },
        };

        protected IEnumerable<ThermalZone> ThermalZones { get; set; } = GetOsTemperatureSensors();

        public WorkerLinuxSensors(ILogger<WorkerLinuxSensors> logger, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ProgramConfiguration> programConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(logger, mqttConfiguration, programConfiguration, mqttClient) { }

        protected override async Task PreExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var thermalZone in ThermalZones)
            {
                SensorsList.Add(
                    new Sensor
                    {
                        SensorClass = SensorClass.temperature,
                        Device = new Device { Name = "OS-Sensor", Model = "ThermalSensor", ViaDevice = ProgramConfiguration.ServiceName }
                    });
            }

            await base.PreExecuteAsync(stoppingToken);
        }

        protected override async Task PostExecuteAsync(CancellationToken stoppingToken)
        {
            WorkingTasks.Add(Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await SendWorkerHeartBeat();
                    await Task.Delay(SendTimeout, stoppingToken);
                }
            }));

            await Task.CompletedTask;
        }

        private async Task SendWorkerHeartBeat()
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                return;

            try
            {
                var deviceId = Devices[0];

                //var procUtilization = GetProcUtilization();
                //var memoryUsage = GetMemoryUsage();
                //var diskUsage = GetDiskUsage();
                var osTemps = GetThermalZonesValues();

                var payload = JObject.FromObject(new
                {
                    Id = deviceId,
                    name = $"OS_{deviceId}"
                });

                foreach (var (Id, Value) in osTemps)
                {
                    // find sensor by valueName
                    var sensor = SensorsList.Single(e => e.SensorClass.ValueName() == $"temp_{Id}");
                    
                    // add sensor value into payload
                    payload.Add(new JProperty(sensor.SensorClass.ValueName(), Value));
                }

                // send message
                await MqttClient.PublishAsync(
                    $"{string.Format(MqttConfiguration.TopicBase, ProgramConfiguration.ServiceName)}/{deviceId}",
                    payload.ToString(),
                    MqttConfiguration.MqttQosLevel);

                Logger.LogInformation($"WorkerLinuxSensors send message: {payload} at {DateTimeOffset.Now}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"WorkerLinuxSensors error at {DateTimeOffset.Now}");
            }

        }

        private IEnumerable<(string Id, string Value)> GetThermalZonesValues() =>
            ThermalZones
                .Select(e => (
                    e.Id,
                    (double.Parse(new FileInfo(Path.Combine(e.Path, "temp")).OpenText().ReadLine()) / 1000).ToString()
                ));

        private static IEnumerable<ThermalZone> GetOsTemperatureSensors()
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                return Enumerable.Empty<ThermalZone>();

            return Directory
                .EnumerateDirectories(ThermalZonesPath, "thermal_zone*")
                .Select(e => new ThermalZone
                {
                    Id = Regex.Match(e, @"\d+$").Value,
                    Path = e,
                    Type = new FileInfo(Path.Combine(e, "type")).OpenText().ReadLine()
                }); 
        }
    }
}
