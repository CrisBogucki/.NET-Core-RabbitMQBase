using RabbitMqBase.Type;

namespace RabbitMqBase.Method
{
    public interface IBaseAsyncProducer
    {
        void Publish(string exchange, string routingKey, TRequest request);

        string Call(string exchange, string routingKey, TRequest request);
    }

}
