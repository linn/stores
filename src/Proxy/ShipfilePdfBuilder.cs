namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;
    using Linn.Stores.Domain.LinnApps;
    using PuppeteerSharp;
    using Scriban;

    public class ShipfilePdfBuilder : IShipfilePdfBuilder
    {
        public async Task<Stream> BuildPdf(ConsignmentShipfile shipfile)
        {
            Browser browser = await Puppeteer.LaunchAsync(new LaunchOptions
                                                              {
                                                                  Args = new[] { "--no-sandbox" },
                                                                  Headless = true
                                                              });
            Page page = await browser.NewPageAsync();
            var template = Template.Parse("Hello {{name}}!");
            var result = await template.RenderAsync(new { Name = "World" });

            await page.SetContentAsync(result);

            var pdfOptions = new PdfOptions { Landscape = true };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;
            
            await browser.CloseAsync();
            
            return pdfStream;
        }
    }
}
