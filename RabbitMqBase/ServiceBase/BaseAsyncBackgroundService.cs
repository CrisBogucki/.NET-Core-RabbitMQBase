using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMqBase.Attribute;

namespace RabbitMqBase.ServiceBase
{
    public abstract class BaseAsyncBackgroundService : BackgroundService
    {
        protected readonly ILogger<BaseAsyncBackgroundService> Logger;
        protected IModel Channel;
        protected string QueueName;

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private string _routingKey;
        private string _exchange;

        protected BaseAsyncBackgroundService(ILogger<BaseAsyncBackgroundService> logger)
        {
            Logger = logger;
        }

        private Service GetOperationSettings()
        {
            return GetType().GetCustomAttribute<Service>();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceName = Tools.GetAppSettingsValueString("ServiceConf", "Name");
            var operationAttr = GetOperationSettings();

            _routingKey = $"{serviceName}.{operationAttr.RoutingKey}.{operationAttr.VerMajor}.{operationAttr.VerMinor}";
            _exchange = Tools.GetAppSettingsValueString("Rabbit", "Exchange");

            Console.WriteLine($"info: RoutingKey : {_routingKey}");
            Console.WriteLine($"info: Description: {GetOperationSettings().Description}");

            try
            {
                _connectionFactory = new ConnectionFactory
                {
                    UserName = Tools.GetAppSettingsValueString("Rabbit", "User"),
                    Password = Tools.GetAppSettingsValueString("Rabbit", "Pass"),
                    VirtualHost = Tools.GetAppSettingsValueString("Rabbit", "VHost"),
                    DispatchConsumersAsync = true
                };
            }
            catch (BrokerUnreachableException e)
            {
                Logger.LogError(e?.Message, e);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                Logger.LogError(default, e, e.Message);
                throw new Exception(e.Message);
            }

            _connection = _connectionFactory.CreateConnection();
            Channel = _connection.CreateModel();

            Channel.ExchangeDeclare(exchange: _exchange, type: "topic");

            var args = new Dictionary<string, object> {{"x-message-ttl", 60000}};

            QueueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queue: QueueName, exchange: _exchange, routingKey: _routingKey, args);

            Channel.BasicQos(0, 1, false);

            Console.WriteLine($"info: Queue      : {QueueName}");
            Console.WriteLine($"info: ---------------------------------------------");
            return base.StartAsync(cancellationToken);
        }

        public override async Task<Task> StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            Logger.LogInformation("RabbitMQ connection is closed");
            return base.StopAsync(cancellationToken);
        }
    }
}
