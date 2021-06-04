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
            Stream pdfAttachment)
        {
            var smtpHost = ConfigurationManager.Configuration["SMTP_HOSTNAME"];
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));

            if (cc != null)
            {
                foreach (var entry in cc)
                {
                    message.Cc.Add(new MailboxAddress(entry["name"], entry["address"]));
                }
            }

            if (bcc != null)
            {
                foreach (var entry in bcc)
                {
                    message.Bcc.Add(new MailboxAddress(entry["name"], entry["address"]));
                }
            }

            message.Subject = subject;

            var emailBody = new TextPart("plain")
                               {
                                   Text = body
                               };

            using (var stream = pdfAttachment)
            {
                byte[] buffer = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Flush();
                stream.Read(buffer, 0, (int)stream.Length);
                var content = new MimeContent(stream);
                var a = new MimePart("application", "pdf")
                                        {
                                            Content = content,
                                            FileName = "Shipfile.pdf"
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
