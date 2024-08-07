using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RiverStateReporting.Services
{
    /// <summary>
    /// Implementation of IEmailSender as a service to send alert emails
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpClient _client;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _client = new SmtpClient
            {
                Host = configuration["EmailSettings:MailServer"],
                Port = int.Parse(configuration["EmailSettings:MailPort"]),
                EnableSsl = bool.Parse(configuration["EmailSettings:EnableSsl"]),
                Credentials = new NetworkCredential(
            configuration["EmailSettings:Sender"],
            configuration["EmailSettings:Password"]
        )
            };
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("adress@example.com"), // Insert your e-mail adress
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await _client.SendMailAsync(mailMessage);
        }
    }
}
