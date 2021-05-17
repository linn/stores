namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;
    using System.IO;

    using Linn.Common.Configuration;
    using Linn.Stores.Domain.LinnApps;

    using MailKit.Net.Smtp;

    using MimeKit;

    public class EmailService : IEmailService
    {
        public void SendEmail(
            string toAddress,
            string toName,
            IEnumerable<Dictionary<string, string>> cc,
            IEnumerable<Dictionary<string, string>> bcc,
            string fromAddress,
            string fromName,
            string subject,
            string body,
            Stream attachment)
        {
            var smtpHost = ConfigurationManager.Configuration["SMTP_HOSTNAME"];
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(toName, toAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));

            foreach (var entry in cc)
            {
                message.Cc.Add(new MailboxAddress(entry["name"], entry["address"]));
            }

            foreach (var entry in bcc)
            {
                message.Bcc.Add(new MailboxAddress(entry["name"], entry["address"]));
            }

            message.Subject = subject;

            var emailBody = new TextPart("plain")
                               {
                                   Text = body
                               };

            using (Stream ms = attachment)
            {
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Flush();
                ms.Read(buffer, 0, (int)ms.Length);
                var content = new MimeContent(ms);
                var a = new MimePart("application", "pdf")
                                        {
                                            Content = content,
                                        };

                var multipart = new Multipart("mixed") { emailBody, a };

                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpHost, 25, false);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}
