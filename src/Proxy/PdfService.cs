namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using PuppeteerSharp;

    public class PdfService : IPdfService
    {
        private readonly SemaphoreSlim semaphore;

        public PdfService(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore;
        }

        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape)
        {
            var browser = Puppeteer.LaunchAsync(new LaunchOptions
                                      {
                                          Args = new[]
                                                     {
                                                         "--no-sandbox"
                                                     },
                                          Headless = true
                                      }).Result;

            await this.semaphore.WaitAsync();
            
            var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions { Landscape = landscape };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await page.CloseAsync();

            this.semaphore.Release();

            return pdfStream;
        }
    }
}
