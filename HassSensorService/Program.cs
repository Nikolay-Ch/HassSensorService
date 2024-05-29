using HassDeviceBaseWorkers;
using HassDeviceWorkers.ModBus;
using HassMqttIntegration;
using HassSensorServiceExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NReco.Logging.File;
using Syslog.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[assembly: AssemblyVersion("1.0.*")]

namespace HassSensorService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = AssemblyName.GetAssemblyName(assembly.Location).Version?.ToString() ?? "";

            Console.WriteLine($"Main: starting. Version: {version}");
            IHost? host = null;
            ILogger<Program>? logger = null;
            try
            {
                List<string> loadedWorkers = [];

                host = CreateHostBuilder(args, loadedWorkers).Build();

                logger = host.Services.GetRequiredService<ILogger<Program>>();

                logger?.LogInformation("Main: Load config done...");

                foreach (var worker in loadedWorkers)
                    logger?.LogInformation("Main: Worker {type} loaded", worker);

                logger?.LogInformation("Main: Waiting for RunAsync to complete");

                await host.StartAsync();

                await host.WaitForShutdownAsync();

                logger?.LogInformation("Main: RunAsync has completed");
            }
            finally
            {
                logger?.LogInformation("Main: stopping");

                if (host is IAsyncDisposable d) await d.DisposeAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, List<string> loadedWorkers) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg)=>
                {
                    var env = ctx.HostingEnvironment;

                    cfg.AddJsonFile("appsettings.json", true, false)
                        .AddJsonFile("./config/appsettings.json", true, false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, false)
                        .AddEnvironmentVariables()
                        ;
                })
                .ConfigureLogging((ctx, logging) =>
                {
                    var slConfig = ctx.Configuration.GetSection("SyslogSettings");
                    if (slConfig != null)
                    {
                        var settings = new SyslogLoggerSettings();
                        slConfig.Bind(settings);

                        // Configure structured data here if desired.
                        logging.AddSyslog(settings);
                    }

                    // add logging to file capability
                    logging.AddFile(ctx.Configuration.GetSection("Logging"));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMemoryCache();

                    services.Configure<MqttConfiguration>(hostContext.Configuration.GetSection(nameof(MqttConfiguration)));
                    services.Configure<WorkersConfiguration>(hostContext.Configuration.GetSection(nameof(WorkersConfiguration)));
                    services.Configure<ModbusGatewayConfiguration>(hostContext.Configuration.GetSection(nameof(ModbusGatewayConfiguration)));
                    services.AddSingleton(typeof(IMqttClientForMultipleSubscribers), typeof(MqttClientForMultipleSubscribers));

                    // get all workers
                    var workers = hostContext
                        .Configuration
                        .GetSection("Workers")
                        .GetChildren()
                        .Where(e => e.GetChildren().First().Key != null)
                        .Select(workerConfig => new
                        {
                            workerType = workerConfig.Key ?? "",
                            workerParams = workerConfig
                                .GetChildren()
                                .ToDictionary(e => e.Key, e => e.Value)
                        });

#if DEBUG
                    var provider = (((ConfigurationRoot)hostContext.Configuration).Providers).Last();
                    foreach (var key in provider.GetFullKeyNames(null, []).OrderBy(p => p))
                        if (provider.TryGet(key, out var value))
                            Console.WriteLine($"{key}={value}");
#endif

                    if (!workers.Any())
                    {
                        Console.WriteLine("Warning! No workers found. Program do nothing...");
                        return;
                    }

                    // add each worker as a service and pass deviceId parameter to it
                    foreach (var worker in workers)
                    {
                        try
                        {
                            var t = Type.GetType($"{worker.workerType}, HassDeviceWorkers", true);

                            loadedWorkers.Add(worker.workerType);

                            if (t != null)
                            {
                                services.AddTransient(typeof(IHostedService),
                                (serviceProvider) => ActivatorUtilities.CreateInstance(serviceProvider, t, worker.workerParams));

                                Console.WriteLine($"Worker: {worker.workerType} has been loaded...");
                            }
                            else
                                Console.WriteLine($"Error loading worker from configuration. {worker.workerType}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error loading worker {worker.workerType} from configuration. {ex.Message}");
                        }
                    }
                });
    }
}
