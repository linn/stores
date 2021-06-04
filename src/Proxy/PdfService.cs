namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using PuppeteerSharp;

    public class PdfService : IPdfService
    {
        private readonly Browser browser;

        public PdfService(Browser browser)
        {
            this.browser = browser;
        }

        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape)
        {
            var page = await this.browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions { Landscape = landscape };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await this.browser.CloseAsync();

            return pdfStream;
        }
    }
}
