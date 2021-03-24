using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMqBase.ServiceBase;
using RabbitMqBase.Method;

namespace Service
{
    [RabbitMqBase.Attribute.Service("service.log.all", "Service for logging all")]
    public class ServiceLogAll : BaseAsyncService<RequestModel>
    {
        private readonly IBaseAsyncProducer _baseAsyncProducer;

        public ServiceLogAll(ILogger<ServiceLogAll> logger, IBaseAsyncProducer baseAsyncProducer) : base(logger)
        {
            _baseAsyncProducer = baseAsyncProducer;
        }

        protected override Task ConsumerHandleAsync(RequestModel request)
        {
            Logger.LogInformation("ServiceLogAll");
            Logger.LogInformation("Received request       <-| " + request.body);
            var payload = new RequestModel {body = request.body};
            Logger.LogInformation("Send request           |-> " + payload);
            var g = _baseAsyncProducer.Call("log", "log.service.log.info.1.0", payload);
            Logger.LogInformation("Recived sended request <-| " + g);
            return Task.CompletedTask;
        }
    }
}
