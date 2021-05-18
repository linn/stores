namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using PuppeteerSharp;
    using Scriban;

    public class PdfBuilder : IPdfBuilder
    {
        public async Task<Stream> BuildPdf(object model, string pathToTemplate)
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

            var templateString = await File.ReadAllTextAsync(pathToTemplate);
            var template = Template.Parse(templateString);

            var result = await template.RenderAsync(model);

            await page.SetContentAsync(result);

            var pdfOptions = new PdfOptions { Landscape = true };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await browser.CloseAsync();

            return pdfStream;
        }
    }
}
