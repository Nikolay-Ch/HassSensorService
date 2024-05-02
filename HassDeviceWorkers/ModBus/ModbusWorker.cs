using Common;
using HassDeviceBaseWorkers;
using HassMqttIntegration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HassDeviceWorkers.ModBus
{
    /// <summary>
    /// Base class for working with Modbus
    /// It syncronyze access to Modbus for multiple readers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ModbusWorker<T> : DeviceBaseWorker<T>
    {
        protected ModbusGatewayConfiguration ModbusGatewayConfiguration { get; }
        protected ILoggerFactory LoggerFactory { get; }

        public ModbusWorker(string deviceId, ILoggerFactory loggerFactory, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient) :
            base(deviceId, loggerFactory.CreateLogger<T>(), workersConfiguration, mqttConfiguration, mqttClient)
        {
            LoggerFactory = loggerFactory;
            LoggerFactory.CreateLogger<ModbusWorker<T>>();
            ModbusGatewayConfiguration = modbusGatewayConfiguration.Value;
        }

        protected override async Task PostSendConfigurationAsync(CancellationToken stoppingToken)
        {
            WorkingTasks.Add(Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var mrr = new ModbusRegisterReader(ModbusGatewayConfiguration, LoggerFactory.CreateLogger<ModbusRegisterReader>());
                        SendWorkerHeartBeat(mrr).Wait();
                    }
                    catch (Exception ex)
                    {
                        Logger?.LogErrorStack(ex);
                    }

                    await Task.Delay(ModbusGatewayConfiguration.SendTimeout, stoppingToken);
                }
            }, stoppingToken));

            await Task.CompletedTask;
        }

        protected abstract Task SendWorkerHeartBeat(ModbusRegisterReader mrr);
    }
}
