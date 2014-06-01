using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LeadManagement.Service.Contracts;

namespace LeadManagement.Service.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var userName = ConfigurationManager.AppSettings["SMTP_USERNAME"];
            var password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];
            var host = ConfigurationManager.AppSettings["SMTP_HOST"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_PORT"]);

            using (var smtpClient = new SmtpClient(host, port))
            {
                smtpClient.Credentials = new NetworkCredential(userName, password);
                var mailMessage = new MailMessage(userName, to, subject, message) { IsBodyHtml = true };
                smtpClient.Send(mailMessage);
                await Task.Yield();
            }
        }
    }
}
