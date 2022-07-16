using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Confluent.Kafka.SyncOverAsync;
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
                BootstrapServers = _eventHubSettings.Hosts,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.Plaintext,
                MessageTimeoutMs = 15_000
            };
        }

        private ConsumerConfig ConsumerConfig()
        {
            return new ConsumerConfig()
            {
                BootstrapServers = _eventHubSettings.Hosts,
                EnableAutoOffsetStore = true,
                AutoOffsetReset = AutoOffsetReset.Latest,
                GroupId = Guid.NewGuid().ToString(),
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.Plaintext,

            };
        }

        public async Task<IProducer<Null, TVal>> BuildProducer()
        {
            await CreateTopic("mail");

            var producer = new ProducerBuilder<Null, TVal>(ProducerConfig())
                .Build();

            return producer;
        }

        public async Task<IConsumer<Ignore, TVal>> BuildConsumer()
        {
            await CreateTopic("mail");

            var consumer = new ConsumerBuilder<Ignore, TVal>(ConsumerConfig())
                .Build();

            return consumer;
        }

        private async Task CreateTopic(string topic)
        {
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _eventHubSettings.Hosts }).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        new TopicSpecification { Name = topic, ReplicationFactor = 1, NumPartitions = 1 } });
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }
            }
        }
    }
}
