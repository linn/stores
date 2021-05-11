﻿namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using PuppeteerSharp;
    using Scriban;

    public class ShipfilePdfBuilder : IShipfilePdfBuilder
    {
        public async Task<Stream> BuildPdf(ConsignmentShipfile shipfile)
        {
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

            string templateString = await File.ReadAllTextAsync("./views/ShipfileEmailTemplate.html");
            var template = Template.Parse(templateString);

            var result = await template.RenderAsync(
                             new
                                 {
                                     name = shipfile.Consignment.CustomerName
                                 });

            await page.SetContentAsync(result);

            var pdfOptions = new PdfOptions { Landscape = true };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await browser.CloseAsync();

            return pdfStream;
        }
    }
}
