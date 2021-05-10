namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using PuppeteerSharp;

    using RazorEngine;
    using RazorEngine.Templating;

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

            string template = File.ReadAllText("views/TestTemplate.cshtml");
            var result = Engine.Razor.RunCompile(template, "templateKey", null, new { Data = "World" });

            await page.SetContentAsync(result);

            var pdfOptions = new PdfOptions { Landscape = true };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;
            
            await browser.CloseAsync();
            
            return pdfStream;
        }
    }
}
