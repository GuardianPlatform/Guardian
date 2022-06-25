using Guardian.Domain.Settings;
using System.Threading.Tasks;

namespace Guardian.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
