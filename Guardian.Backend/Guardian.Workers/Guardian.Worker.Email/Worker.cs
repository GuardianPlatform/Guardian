using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Guardian.Domain.Settings;
using Guardian.Infrastructure.Communication;
using Guardian.Infrastructure.Email;
using Guardian.Infrastructure.EventHub;
using Newtonsoft.Json;

namespace Guardian.Worker.Email
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailService _emailService;
        private readonly ILoggingApiClient _loggingApiClient;
        private readonly IConsumer<Ignore, string> _consumer;

        public Worker(ILogger<Worker> logger,
            IEventHubBuilder<string> eventHubBuilder,
            IEmailService emailService,
            ILoggingApiClient loggingApiClient)
        {
            _logger = logger;
            _emailService = emailService;
            _loggingApiClient = loggingApiClient;
            _consumer = eventHubBuilder.BuildConsumer().GetAwaiter().GetResult();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("mail");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                while (true)
                {
                    var message = _consumer.Consume(10_000);

                    if (message == null)
                    {
                        break;
                    }

                    var mail = JsonConvert.DeserializeObject<MailRequest>(message.Message.Value);

                    try
                    {
                        await _emailService.SendEmailAsync(mail);

                        await _loggingApiClient.Log($"Sent email: {message.Message.Value}",
                            message.Message.Timestamp.UtcDateTime,
                            cancellationToken: stoppingToken);
                    }
                    catch (Exception e)
                    {
                        await _loggingApiClient.Log($"Problem during sending an email: {JsonConvert.SerializeObject(e)}",
                            message.Message.Timestamp.UtcDateTime,
                            cancellationToken: stoppingToken);
                        throw;
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
