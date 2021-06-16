namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using Microsoft.AspNetCore.Razor.TagHelpers;

    using PuppeteerSharp;

    public class PdfService : IPdfService
    {
        private readonly Browser browser;

        private readonly SemaphoreSlim semaphore;

        public PdfService(Browser browser, SemaphoreSlim semaphore)
        {
            this.browser = browser;
            this.semaphore = semaphore;
        }

        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape)
        {
            await this.semaphore.WaitAsync();
            var page = await this.browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions { Landscape = landscape };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await page.CloseAsync();

            this.semaphore.Release();

            return pdfStream;
        }
    }
}
