using System.Net.Mail;
using Winmark.Client.Services.Interfaces;

namespace Winmark.Client.Services
{
    public class EmailService : IEmailService
    {
        public void SendMail(MailMessage mailMessage)
        {
            var smtp = new SmtpClient();

#if DEBUG
            smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            smtp.PickupDirectoryLocation = "C:\\temp";
#endif

            smtp.Send(mailMessage);
        }
    }
}
