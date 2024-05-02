using HassSensorConfiguration;
using System;
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
                        Name = "NAME",
                        Model = "MODEL NAME",
                        Manufacturer = "MANUFACTURER",
                        ViaDevice = "VIA DEVICE",
                        Identifiers = ["XXXXXXXXXXXX"],
                        Connections = [["mac", "XXXXXXXXXXXX"]]
                    },
                    StateClass = StateClass.Measurement,
                    DeviceClassDescription = new DeviceClassDescription()
                });

            var topic = sensor.GetConfigTopic();
            var sensorInfo = JsonSerializer.Serialize(sensor);

            Console.WriteLine(topic);
            Console.WriteLine(sensorInfo);
        }
    }
}
