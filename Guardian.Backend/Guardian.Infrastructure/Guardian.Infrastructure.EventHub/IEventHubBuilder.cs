using Confluent.Kafka;

namespace Guardian.Infrastructure.EventHub
{
    public interface IEventHubBuilder<TVal>
    {
        IProducer<Null, TVal> BuildProducer();
        IConsumer<Ignore, TVal> BuildConsumer();
    }
}