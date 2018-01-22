using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mail
{
    public static class MessageService
    {
        public static async Task SendEmailAsync(string server, int port, bool useSsl, string login, string password, string fromName, string fromEmail, string toName, string toEmail, string subject, string message)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(fromName, fromEmail));
            msg.To.Add(new MailboxAddress(toName, toEmail));
            msg.Subject = subject;
            msg.Body = new TextPart("html")
            {
                Text = message
            };

            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(server, port, useSsl);

                if (login != null && password != null)
                {
                    await client.AuthenticateAsync(login, password);
                }

                await client.SendAsync(msg);
                await client.DisconnectAsync(true);
            }
        }
    }
}
