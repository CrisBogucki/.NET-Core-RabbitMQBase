using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMqBase.ServiceBase;
using RabbitMqBase.Type;

namespace Service
{
    [RabbitMqBase.Attribute.Service("service.log.info","Service for logging info")]
    public class ServiceLogInfo : BaseAsyncService<RequestModel, ResponseModel>
    {
        public ServiceLogInfo(ILogger<ServiceLogInfo> logger) : base(logger)
        { }

        protected override async Task<TResponse> ConsumerHandleAsync(RequestModel request)
        {
            Logger.LogInformation("ServiceLogInfo");
            var payload = new ResponseModel{body = $"Hellow from service info {request.body}"};
            return await Task.FromResult(payload);
        }
    }
}
