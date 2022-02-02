using HassDeviceBaseWorkers;
using HassMqttIntegration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
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
        private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

        protected ModbusGatewayConfiguration ModbusGatewayConfiguration { get; }

        public ModbusWorker(string deviceId, ILogger<T> logger, IOptions<WorkersConfiguration> workersConfiguration, IOptions<MqttConfiguration> mqttConfiguration, IOptions<ModbusGatewayConfiguration> modbusGatewayConfiguration, IMqttClientForMultipleSubscribers mqttClient) :
            base(deviceId, logger, workersConfiguration, mqttConfiguration, mqttClient)
        {
            ModbusGatewayConfiguration = modbusGatewayConfiguration.Value;
        }

        protected override async Task PostSendConfigurationAsync(CancellationToken stoppingToken)
        {
            WorkingTasks.Add(Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await semaphoreSlim.WaitAsync();
                    try
                    {
                        using var mrr = new ModbusRegisterReader { SerialPort = CreateSerialPort() };
                        await SendWorkerHeartBeat(mrr);

                        Thread.Sleep(500); // waiting to free Modbus
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex,
                            $"{new StackTrace(ex).GetFrame(0).GetMethod().Name} error '{ex.Message}' at {DateTimeOffset.Now}");
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }

                    await Task.Delay(ModbusGatewayConfiguration.SendTimeout, stoppingToken);
                }
            }, stoppingToken));

            await Task.CompletedTask;
        }

        protected abstract Task SendWorkerHeartBeat(ModbusRegisterReader mrr);

        private ISerialPort CreateSerialPort() =>
            new RemoteSerialPort(
                ModbusGatewayConfiguration.GatewayAddress,
                ModbusGatewayConfiguration.GatewayPort) { ReadTimeout = 3000 };
    }
}
