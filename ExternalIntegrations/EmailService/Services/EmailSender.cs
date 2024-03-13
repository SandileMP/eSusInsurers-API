using EmailService.Interfaces;
using EmailService.Models;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace EmailService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MailMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MailMessage(new MailAddress(_emailConfig.From), new MailAddress(message.To));

            emailMessage.Subject = message.Subject;

            emailMessage.IsBodyHtml = true;

            emailMessage.Body = message.Content;

            if (message.Attachments != null && message.Attachments.Any())
            {
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        emailMessage.Attachments.Add(new Attachment(ms, attachment.ContentType));
                    }

                }
            }

            return emailMessage;
        }


        private async Task SendAsync(MailMessage mailMessage)
        {

            using (var client = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
            {
                try
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
