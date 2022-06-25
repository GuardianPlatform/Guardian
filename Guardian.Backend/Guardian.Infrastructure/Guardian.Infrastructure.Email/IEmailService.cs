using System.Threading.Tasks;
using Guardian.Domain.Settings;

namespace Guardian.Infrastructure.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}