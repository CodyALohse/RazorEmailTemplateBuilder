using System.Net.Mail;

namespace Winmark.Client.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMail(MailMessage mailMessage);
    }
}
