using HassSensorConfiguration;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var sensor = new AnalogSensorFactory().CreateComponent(
                new AnalogSensorDescription()
                {
                    Device = new Device()
                    {
                        Name = "ABN03",
                        Model = "ABSensor N03",
                        Manufacturer = "April Brother",
                        ViaDevice = "Bla-Bla-Bla",
                        Identifiers = new List<string>() { "XXXXXXXXXXXX" },
                        Connections = new List<List<string>>() { new List<string>() { "mac", "XXXXXXXXXXXX" } }
                    },
                    StateClass = StateClass.Measurement
                });

            var topic = sensor.GetConfigTopic();
            var sensorInfo = JsonSerializer.Serialize(sensor);

            Console.WriteLine(topic);
            Console.WriteLine(sensorInfo);
        }
    }
}
