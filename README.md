# .NET-Core-RabbitMQBase
RabbitMQBase for Microservice as WorkerService

## Config

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
