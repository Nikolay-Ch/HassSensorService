# AprilBeaconsHomeAssistantIntegrationService
AprilBeacon BLE devices HomeAssistant .net core integration service

AprilBeacon Bluetooth Low Enegry devices integration service.
Works with OpenMQTTGateway that get advertising data from devices and publish it to MQTT-server
So, my service listen target MQTT-topic, read raw-messages from devices then parse it and then resend message into MQTT-topic
At the start, service send configuration message, to create devices in the HomeAssistant

Service get configuration from json-config file or from command line.

Example of the config file:
{
  "MqttConfiguration": {
    "MqttUri": "aaa.bbb.ccc.ddd",
    "MqttUser": "mqtt_user",
    "MqttUserPassword": "mqtt_password",
    "MqttPort": 1883,
    "MqttSecure": false,
    "MqttQosLevel": "AtMostOnce",
    "TopicBase": "Topic, in that OpenMQTTGateway publish messages from BLE devices"
  },
  "ProgramConfiguration": {
    "AprilBeaconDevicesList": [ "device_id" ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
