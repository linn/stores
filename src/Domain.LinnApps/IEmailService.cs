namespace Linn.Stores.Domain.LinnApps
{
    using SelectPdf;

    public interface IEmailService
    {
        void SendEmail(
            string toAddress, 
            string toName,
            string fromAddress,
            string fromName,
            string subject, 
            string body, 
            PdfDocument pdfAttachment);
    }
}
