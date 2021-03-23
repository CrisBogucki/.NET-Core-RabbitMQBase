using RabbitMqBase.Type;

namespace Service
{
    public class RequestModel : TRequest
    {
        public string body { get; set; }

        public override string ToString()
        {
            return $"{body}";
        }
    }
}
