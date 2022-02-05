using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;
using HassDeviceBaseWorkers;
using HassMqttIntegration;
using HassSensorConfiguration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceWorkers
{
    public class ThermalZone
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Name => $"OS-TEMP-{Type}{Id}"; // for identification purposes...
    }

    /// <summary>
    /// Send Unix-metrict to HomeAssistant
    /// </summary>
    class WorkerLinuxSensors : DeviceBaseWorker<WorkerLinuxSensors>
    {
        public int SendTimeout { get; set; } = 30000;

        protected override string DeviceId { get; } = new DeviceIdBuilder()
                .AddMachineName()
                .AddOsVersion()
                .UseFormatter(new StringDeviceIdFormatter(new PlainTextDeviceIdComponentEncoder()))
                .ToString();

        protected static string ThermalZonesPath { get; } = "/sys/class";
        protected static string CpuUtilizationPath { get; } = "/proc/stat";
        protected static string OsName { get; } = Environment.MachineName;

        protected IEnumerable<ThermalZone> ThermalZones { get; set; } = GetOsTemperatureSensors();
        protected Device Device { get; init; }

        public WorkerLinuxSensors(string deviceId, ILogger<WorkerLinuxSensors> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IMqttClientForMultipleSubscribers mqttClient)
            : base(deviceId, logger, workersConfiguration, mqttConfiguration, mqttClient)
        {
            var macAddresses = NetworkInterface
                .GetAllNetworkInterfaces()
                .Select(e => new List<string> { "mac", e.GetPhysicalAddress().ToString() })
                .ToList();

            var ipAddresses = Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .Where(e => e.AddressFamily == AddressFamily.InterNetwork)
                .Select(e => new List<string> { "ip", e.ToString() })
                .ToList();

            var allAddresses = new List<List<string>>();
            allAddresses.AddRange(macAddresses);
            allAddresses.AddRange(ipAddresses);

            var sensorFactory = new AnalogSensorFactory();
            Device = new()
            {
                Name = "OS-Sensor",
                Model = "V1",
                ViaDevice = WorkersConfiguration.ServiceName,
                Identifiers = new() { DeviceId },
                Connections = allAddresses
            };

            ComponentList.Add(sensorFactory.CreateComponent(
                new AnalogSensorDescription
                {
                    DeviceClassDescription = new DeviceClassDescription
                    {
                        ValueName = "load",
                        UnitOfMeasures = "%"
                    },
                    Device = Device,
                    SensorName = "CPU_Load",
                    UniqueId = $"{Environment.MachineName}-CPU-Load"
                }));

            //// Memory Usage (read data from /proc/meminfo https://vitux.com/5-ways-to-check-available-memory-in-ubuntu/)
            //SensorsList.Add(sensorFactory.CreateSensor(
            //    new DeviceClassDescription { DeviceClass = null, ValueName = "mem", UnitOfMeasures = "Mb" },
            //    device: Device, sensorName: "MEM_Usage", uniqueId: $"{Environment.MachineName}-MEM-Usage"));
            
            //SensorsList.Add(sensorFactory.CreateSensor(
            //    new DeviceClassDescription { DeviceClass = null, ValueName = "disk", UnitOfMeasures = "Mb" },
            //    device: Device, sensorName: "Disk_Usage", uniqueId: $"{Environment.MachineName}-Disk-Usage"));
        }

        protected override async Task PreSendConfigurationAsync(CancellationToken stoppingToken)
        {
            foreach (var thermalZone in ThermalZones)
                ComponentList.Add(
                    new AnalogSensorFactory().CreateComponent(
                        new AnalogSensorDescription
                        {
                            DeviceClassDescription = DeviceClassDescription.Temperature,
                            Device = Device,
                            UniqueId = $"{Environment.MachineName}-{thermalZone.Name}"
                        }));

            await base.PreSendConfigurationAsync(stoppingToken);
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
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                return;

            try
            {
                var procUtilization = GetProcUtilization();
                //var memoryUsage = GetMemoryUsage();
                //var diskUsage = GetDiskUsage();
                var osTemps = GetThermalZonesValues();

                var payload = CreatePayloadObject();

                var procUtilizationSensor = ComponentList.Single(e => e.Name == "CPU_Load");
                payload.Add(new JProperty(procUtilizationSensor.DeviceClassDescription.ValueName, procUtilization.ToString("N2")));

                foreach (var (Name, Value) in osTemps)
                {
                    // find sensor by valueName
                    var sensor = ComponentList.Single(e => e.UniqueId == $"{Environment.MachineName}-{Name}");
                    
                    // add sensor value into payload
                    payload.Add(new JProperty(sensor.DeviceClassDescription.ValueName, Value));
                }

                // send message
                await SendDeviceInformation(ComponentList[0], payload);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{typeName} error at {time}", GetType().Name, DateTimeOffset.Now);
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// TemperatureХХ (scans for all thermal_zone sensors in /sys/class/thermal/thermal_zoneХХ https://askubuntu.com/questions/229929/correct-sensor-location-to-use-for-temperature-monitor-in-lxpanel)
        /// </summary>
        /// <returns></returns>
        private IEnumerable<(string Id, string Value)> GetThermalZonesValues() =>
            ThermalZones
                .Select(e => (
                    e.Id,
                    (double.Parse(new FileInfo(Path.Combine(e.Path, "temp")).OpenText().ReadLine()) / 1000).ToString()
                ));


        private int lastTotalTime = 0;
        private int lastWorkTime = 0;
        /// <summary>
        /// CPU utilization - read data from /proc/stat
        /// https://rosettacode.org/wiki/Linux_CPU_utilization
        /// https://stackoverflow.com/questions/3017162/how-to-get-total-cpu-usage-in-linux-using-c
        /// </summary>
        /// <returns></returns>
        private double GetProcUtilization()
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                return 0;

            var cpuUtilization = File.ReadLines(CpuUtilizationPath).First();

            var cpuVals = cpuUtilization
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Skip(1)
                .Select(e => int.Parse(e))
                .ToList();

            var totalTime = cpuVals.Sum();
            var workTime = cpuVals.Take(3).Sum();

            Logger.LogTrace("Reading CPU utilization: '{cpuUtilization}', {totalTimeName}: {totalTime}, {workTimeName}: {workTime}", cpuUtilization, nameof(totalTime), totalTime, nameof(workTime), workTime);

            if (lastTotalTime == 0)
                return -1;

            var workOverPeriod = workTime - lastWorkTime;
            var totalOverPeriod = totalTime - lastTotalTime;

            lastWorkTime = workTime;
            lastTotalTime = totalTime;

            var cpuUsage = workOverPeriod / (double)totalOverPeriod * 100;

            Logger.LogTrace("Calculated CPU usage: {cpuUsage}", cpuUsage);

            return cpuUsage;
        }

    }
}
