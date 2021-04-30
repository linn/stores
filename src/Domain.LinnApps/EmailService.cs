namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;
    using System.Linq;

    using Linn.Common.Configuration;

    using MailKit.Net.Smtp;

    using MimeKit;

    using PdfSharp.Drawing;
    using PdfSharp.Pdf;

    public class EmailService : IEmailService
    {
        public void SendEmail(
            string toAddress,
            string toName,
            string fromAddress,
            string fromName,
            string subject,
            string body,
            PdfDocument pdfAttachment)
        {
            var smtpHost = ConfigurationManager.Configuration["SMTP_HOSTNAME"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(toName, toAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));

            message.Subject = subject;

            var emailBody = new TextPart("plain")
                               {
                                   Text = body
                               };

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            gfx.DrawString(
                "Hello, World!", 
                font, 
                XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

           MimeContent content;
           using (MemoryStream ms = new MemoryStream())
           {
               document.Save(ms, false);
               byte[] buffer = new byte[ms.Length];
               ms.Seek(0, SeekOrigin.Begin);
               ms.Flush();
               ms.Read(buffer, 0, (int)ms.Length);
               content = new MimeContent(ms);
               var attachment = new MimePart("application", "pdf")
                                    {
                                        Content = content,
                                    };

               var multipart = new Multipart("mixed") { emailBody, attachment };

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
