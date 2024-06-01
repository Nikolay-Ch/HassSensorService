# HassSensorService
Custom devices HomeAssistant .net core integration service. Works both on Windows or Linux systems. You can also run it on **Raspberry Pi** or **Orange Pi** microcontrollers, that uses **Raspbian** or **Armbian**. In the this case, you need to add a **Publish profile** in solution with target platform: **ARM-Linux**.

Now support BLE and Modbus devices. Also support custom devices like OS parameters (memory, disk space, processor load). See HassDeviceWorkers folder for details.

Device Workers:
* **WorkerLinuxSensors** - Linux OS-devices support. Like memory, disk space, processor load etc. Beta - not tested.
* **RsGzwsN01Worker** - Worker, that uses Modbus device ([RS-GZWS-N01](https://www.google.com/search?q=RS-GZWS-N01)).
* **Sdm120mWorker** - Worker for Eastron SDM120 Modbus energy meter.
* **Zm1940d9yWorker** - Worker for Modbus 3-phase energy meter ZM194-D9Y.
* **WorkerABN03** - AprilBeacon Bluetooth Low Enegry devices integration service. Support hardware: [ABN03](https://wiki.aprbrother.com/en/ABSensor.html#absensor-n03)
* **WorkerBTH01** - BLE Tuya [BTH01](https://pvvx.github.io/BTH01/) device with custom [PVVX](https://github.com/pvvx/THB2?tab=readme-ov-file) firmware	Support.
* **HPiLO4DataWorker** - Hewlett Packard iLO Health sensors... Set URL, User and Password in configuration file and health iLO sensors of your server will appear in the Home Assistant

**WorkerBTH01**, **WorkerABN03** uses MQTT-advertising packets from [OpenMqttGateway](https://github.com/1technophile/OpenMQTTGateway). They listen target MQTT-topic, read raw-messages from devices then parse it and at last send message into MQTT-topic.

All workers at the start send configuration message (that contains information about sensors in device), to create devices in the HomeAssistant.

Service get configuration from json-config file, command line parameters or environment variables.
For all configuration parameters see [configuraion file](HassSensorService/appsettings.json)
