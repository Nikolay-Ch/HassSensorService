using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<MqttConfiguration>(hostContext.Configuration.GetSection("MqttConfiguration"));
                    services.Configure<ProgramConfiguration>(hostContext.Configuration.GetSection("ProgramConfiguration"));
                    services.AddSingleton(typeof(IMqttClientForMultipleSubscribers), typeof(MqttClientForMultipleSubscribers));
                    //services.AddHostedService<WorkerLinuxSensors>();
                    services.AddHostedService<WorkerABN03>();
                });
    }
}
