using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMqBase.ServiceBase;
using RabbitMqBase.Type;

namespace Service
{
    [RabbitMqBase.Attribute.Service("service.log.error","Service for logging errors")]
    public class ServiceLogError : BaseAsyncService<RequestModel, ResponseModel>
    {
        public ServiceLogError(ILogger<ServiceLogError> logger) : base(logger)
        { }

        protected override async Task<TResponse> ConsumerHandleAsync(RequestModel request)
        {
            Logger.LogInformation("ServiceLogError");
            var payload = new ResponseModel{body = $"Hellow from service error {request.body}"};
            return await Task.FromResult(payload);
        }
    }
}
