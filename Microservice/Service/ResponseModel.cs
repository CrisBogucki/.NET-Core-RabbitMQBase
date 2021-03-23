using RabbitMqBase.Type;

namespace Service
{
    public class ResponseModel : TResponse
    {
        public string body { get; set; }

        public override string ToString()
        {
            return $"{body}";
        }
    }
}
