using Confluent.Kafka;
using Guardian.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Guardian.Infrastructure.EventHub
{
    public class EventHubBuilder<TVal> : IEventHubBuilder<TVal>
    {
        private readonly EventHubSettings _eventHubSettings;

        public EventHubBuilder(IOptions<EventHubSettings> eventHubSettings)
        {
            _eventHubSettings = eventHubSettings.Value;
        }

        private ProducerConfig ProducerConfig()
        {
            return new ProducerConfig()
            {
                BootstrapServers = _eventHubSettings.Hosts
            };
        }

        public IProducer<Null, TVal> BuildProducer()
        {
            var producer = new ProducerBuilder<Null, TVal>(ProducerConfig())
                .Build();

            return producer;
        }

        public IConsumer<Ignore, TVal> BuildConsumer()
        {
            var consumer = new ConsumerBuilder<Ignore, TVal>(ProducerConfig())
                .Build();

            return consumer;
        }
    }
}
