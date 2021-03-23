using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMqBase.ServiceBase;
using RabbitMqBase.Method;

namespace Service
{
    [RabbitMqBase.Attribute.Service("service.log.#","Service for logging all")]
    public class ServiceLogAll : BaseAsyncService<RequestModel>
    {
        private readonly IBaseAsyncProducer _baseAsyncProducer;
        public ServiceLogAll(ILogger<ServiceLogAll> logger, IBaseAsyncProducer baseAsyncProducer) : base(logger)
        {
            _baseAsyncProducer = baseAsyncProducer;
        }

        protected override Task ConsumerHandleAsync(RequestModel request)
        {
            var payload = new RequestModel {body = $"Call payload body {request.body}"};
            var g = _baseAsyncProducer.Call("acp.sys", "i-auth.login.1.0", payload);
            Logger.LogWarning(g);
            return Task.CompletedTask;
        }
    }
}
