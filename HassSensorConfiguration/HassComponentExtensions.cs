using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HassSensorConfiguration
{
    public enum HassComponentType
    {
        Unknown = 0,
        BinarySensor,
        Sensor
    }

    public static class HassComponentExtensions
    {
        public static HassComponentType GetComponentTypeFromConfigurationTopic(this string configurationTopic) =>
            configurationTopic.Split('/')[1] switch
            {
                "binary_sensor" => HassComponentType.BinarySensor,
                "sensor" => HassComponentType.Sensor,
                _ => HassComponentType.Unknown,
            };

        public static string GetHassComponentTypeString(this Type componentType)
        {
            if (componentType == typeof(BinarySensor))
                return "binary_sensor";
            if (componentType == typeof(AnalogSensor))
                return "sensor";

            return "unknown";
        }

        public static Type ComponentType(this HassComponentType componentType) =>
            componentType switch
            {
                HassComponentType.Unknown => typeof(object),
                HassComponentType.BinarySensor => typeof(BinarySensor),
                HassComponentType.Sensor => typeof(AnalogSensor),
                _ => typeof(object),
            };


        public static Dictionary<string, string> PropertyMappings => new()
        {
            { "stat_cla", "state_class" },
            { "stat_t", "state_topic" },
            { "uniq_id", "unique_id" },
            { "dev_cla", "device_class" },
            { "pl_on", "payload_on" },
            { "pl_off", "payload_off" },
            { "pl_not_avail", "payload_not_available" },
            { "ic", "icon" },
            { "val_tpl", "value_template" },
            { "unit_of_meas", "unit_of_measurement" },
            { "dev", "device" },
            { "cns", "connections" },
            { "ids", "identifiers" },
            { "mf", "manufacturer" },
            { "mdl", "model" },
            { "sw", "sw_version" },
        };

        public static List<List<string>> AllComponentAddresses
        {
            get
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

                return allAddresses;
            }
        }
    }
}
