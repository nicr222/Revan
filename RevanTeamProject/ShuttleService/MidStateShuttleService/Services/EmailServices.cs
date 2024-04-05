using NuGet.Protocol.Plugins;
using System.Net;
using System.Net.Mail;

namespace MidStateShuttleService.Services
{
    public class EmailServices
    {

        public void SendEmail(string recipiant, string subject, string body)
        {
            // Establish SMTP2GO Client
            SmtpClient client = new SmtpClient("mail.smtp2go.com")
            {
                Port = 2525, // SMTP2GO port number
                Credentials = new NetworkCredential("Username", "Password"),
                EnableSsl = true // SMTP2GO requires SSL
            };

            MailMessage message = new MailMessage("csd", recipiant)
            {
                Subject = subject,
                Body = body
            };

            // Send the email
            client.Send(message);
        }
    }
}
