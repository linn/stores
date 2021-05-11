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
            // launch a headless chrome instance and initialise a page
            Browser browser =
                await Puppeteer.LaunchAsync(new LaunchOptions
                                                {
                                                    Args = new[]
                                                               {
                                                                   "--no-sandbox"
                                                               }, 
                                                    Headless = true
                                                });
            Page page = await browser.NewPageAsync();

            // get the template from file and parse it
            string templateString = await File.ReadAllTextAsync("./views/TestTemplate.html");
            var template = Template.ParseLiquid(templateString);

            // render the template with this anonymous model
            var result = await template.RenderAsync(
                             new
                                 {
                                     Name = "Lewis"
                                 });

            // pass chromium the rendered html to convert to a pdf
            await page.SetContentAsync(result);

            var pdfOptions = new PdfOptions { Landscape = true };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await browser.CloseAsync();

            return pdfStream;
        }
    }
}
