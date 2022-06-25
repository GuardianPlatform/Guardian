using Confluent.Kafka;

namespace Guardian.Service.Contract
{
    public interface IEventHubBuilder<TVal>
    {
        IProducer<Null, TVal> BuildProducer();
        IConsumer<Ignore, TVal> BuildConsumer();

    }
}
