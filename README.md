# .NET-Core-RabbitMQBase
RabbitMQBase for Microservice as WorkerService

### AppSetting.json

```json
{
  "ServiceConf": {
    "Name": "i-sample-microservice"
  },
  "Rabbit": {
    "User": "guest",
    "Pass": "guest",
    "Hosts": [
      "rabbit-1",
      "rabbit-2"
    ],
    "VHost": "/",
    "Exchange": "ex.sys"
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
                    services.AddHostedService<Service02>();
                    services.AddHostedService<Service01>();
                });
            Console.WriteLine($"info: Running microservice {Tools.GetAppSettingsValueString("ServiceConf", "Name")} " +
                              $"version: {Tools.GetVersionString()}");

            Console.WriteLine($"info: Exchange   : {Tools.GetAppSettingsValueString("Rabbit", "Exchange")}");
            Console.WriteLine($"info: ---------------------------------------------");

            return host;
        }

```
