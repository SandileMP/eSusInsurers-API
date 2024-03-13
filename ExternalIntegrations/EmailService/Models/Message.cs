using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Net.Mail;

namespace EmailService.Models
{
    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection? Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection? attachments)
        {
            To = string.Join(", ", to.Select(x => new MailAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
