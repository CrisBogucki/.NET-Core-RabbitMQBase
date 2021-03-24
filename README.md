# .NET-Core-RabbitMQBase
RabbitMQBase for Microservice as WorkerService

### Download and run RabbitMQ from docker 
`docker-compose up -d` 

### AppSetting.json

```json
{
  "ServiceConf": {
    "Name": "i-log"
  },
  "Rabbit": {
    "User": "guest",
    "Pass": "guest",
    "Hosts": [
      "rabbit-1",
      "rabbit-2"
    ],
    "VHost": "/",
    "Exchange": "log"
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

### Main class
```c#
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
```
