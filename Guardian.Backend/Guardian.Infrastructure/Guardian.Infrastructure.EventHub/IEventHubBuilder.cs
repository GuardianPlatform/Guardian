using System.Threading.Tasks;
using Confluent.Kafka;

namespace Guardian.Infrastructure.EventHub
{
    public interface IEventHubBuilder<TVal>
    {
        Task<IProducer<Null, TVal>> BuildProducer();
        Task<IConsumer<Ignore, TVal>> BuildConsumer();
    }
}