namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;

    public interface IEmailService
    {
        void SendEmail(
            string toAddress, 
            string toName,
            string fromAddress,
            string fromName,
            string subject, 
            string body, 
            Stream attachment);
    }
}
