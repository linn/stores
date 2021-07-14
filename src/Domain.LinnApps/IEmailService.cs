namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.IO;

    public interface IEmailService
    {
        void SendEmail(
            string toAddress, 
            string toName,
            IEnumerable<Dictionary<string, string>> cc,
            IEnumerable<Dictionary<string, string>> bcc,
            string fromAddress,
            string fromName,
            string subject, 
            string body, 
            Stream pdfAttachment,
            string attachmentName);
    }
}
