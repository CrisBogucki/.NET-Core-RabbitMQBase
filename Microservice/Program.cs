using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMqBase;
using RabbitMqBase.Method;
using Service;

namespace Microservice
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IBaseAsyncProducer, BaseAsyncProducer>();
                    services.AddHostedService<ServiceLogAll>();
                    services.AddHostedService<ServiceLogInfo>();
                });
            Console.WriteLine($"info: Running microservice {Tools.GetAppSettingsValueString("ServiceConf", "Name")} " +
                              $"version: {Tools.GetVersionString()}");
            Console.WriteLine($"info: Exchange   : {Tools.GetAppSettingsValueString("Rabbit", "Exchange")}");
            Console.WriteLine($"info: ---------------------------------------------");

            return host;
        }
    }
}
