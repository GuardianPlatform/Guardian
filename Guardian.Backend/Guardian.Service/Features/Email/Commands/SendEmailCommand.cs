using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Guardian.Domain.Settings;
using Guardian.Infrastructure.EventHub;
using Newtonsoft.Json;

namespace Guardian.Service.Features.Email.Commands
{
    public class SendEmailCommand : INotification
    {
        public MailRequest MailRequest { get; set; }

        public class SendEmailCommandHandler : INotificationHandler<SendEmailCommand>
        {
            private readonly IProducer<Null, string> _producer;

            public SendEmailCommandHandler(IEventHubBuilder<string> eventHubBuilder)
            {
                _producer = eventHubBuilder.BuildProducer().GetAwaiter().GetResult();
            }

            public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
            {
                await _producer.ProduceAsync("mail", new Message<Null, string>() { Value = JsonConvert.SerializeObject(request.MailRequest) },
                    cancellationToken);
            }
        }
    }
}
