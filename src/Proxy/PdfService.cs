namespace Linn.Stores.Proxy
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    public class PdfService : IPdfService
    {
        public PdfService()
        {
        }

        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape)
        {
            throw new NotImplementedException();
        }
    }
}
