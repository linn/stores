namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using PuppeteerSharp;

    public class PdfService : IPdfService
    {
        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape)
        {
            var browser =
                await Puppeteer.LaunchAsync(new LaunchOptions
                                                {
                                                    Args = new[]
                                                               {
                                                                   "--no-sandbox"
                                                               }, 
                                                    Headless = true
                                                });
            var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions { Landscape = landscape };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await browser.CloseAsync();

            return pdfStream;
        }
    }
}
