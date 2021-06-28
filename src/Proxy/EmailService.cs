namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Amazon.SimpleEmail;
    using Amazon.SimpleEmail.Model;

    using Linn.Common.Configuration;
    using Linn.Stores.Domain.LinnApps;


    using MimeKit;

    public class EmailService : IEmailService
    {
        private readonly IAmazonSimpleEmailService emailService;

        public EmailService(IAmazonSimpleEmailService emailService)
        {
            this.emailService = emailService;
        }

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

            var emailBody = new TextPart("plain") { Text = body };

            using (var stream = pdfAttachment)
            {
                var buffer = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Flush();
                stream.Read(buffer, 0, (int)stream.Length);
                var content = new MimeContent(stream);
                var a = new MimePart("application", "pdf") { Content = content, FileName = "Shipfile.pdf" };

                var multipart = new Multipart("mixed") { emailBody, a };

                message.Body = multipart;

                var response = this.emailService.SendRawEmailAsync(new SendRawEmailRequest
                                                        {
                                                            RawMessage = new RawMessage(GetMessageStream(message))
                                                        });
                Task.WaitAll(response);
            }
        }

        private static MemoryStream GetMessageStream(MimeMessage message)
        {
            var stream = new MemoryStream();
            message.WriteTo(stream);
            return stream;
        }
    }
}
