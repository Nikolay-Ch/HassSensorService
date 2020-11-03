# AprilBeaconsHomeAssistantIntegrationService
AprilBeacon BLE devices HomeAssistant .net core integration service

AprilBeacon Bluetooth Low Enegry devices integration service.
Works with OpenMQTTGateway that get advertising data from devices and publish it to MQTT-server.
So, my service listen target MQTT-topic, read raw-messages from devices then parse it and then resend message into MQTT-topic.
At the start, service send configuration message, to create devices in the HomeAssistant

Support hardware:
[ABN03](https://wiki.aprbrother.com/en/ABSensor.html#absensor-n03)

Service get configuration from json-config file or from command line.
Minimum you need to setup this properties:
* MqttUri
* MqttUser
* MqttUserPassword
* TopicBase
* AprilBeaconDevicesList

Example of the config file:
```json
{
  "MqttConfiguration": {
    "MqttUri": "12.456.789.101",
    "MqttUser": "mqtt_user",
    "MqttUserPassword": "mqtt_password",
    "MqttPort": 1883,
    "MqttSecure": false,
    "MqttQosLevel": "AtMostOnce",
    "TopicBase": "home/OpenMQTTGateway/BTtoMQTT"
  },
  "ProgramConfiguration": {
    "AprilBeaconDevicesList": [ "AABBCC112233", "DDEEFF445566" ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
