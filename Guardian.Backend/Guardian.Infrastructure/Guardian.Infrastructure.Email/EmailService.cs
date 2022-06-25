using Guardian.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using Guardian.Domain;

namespace Guardian.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                // create message
                var email = BuildEmailMessage(mailRequest);

                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        private MimeMessage BuildEmailMessage(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailRequest.From ?? _mailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };
            email.Body = builder.ToMessageBody();

            return email;
        }

    }
}