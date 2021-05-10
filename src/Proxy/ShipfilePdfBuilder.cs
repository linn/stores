namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using Microsoft.AspNetCore.Html;

    using PuppeteerSharp;

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

            var builder = new HtmlContentBuilder();

            builder.AppendFormat("<html> <body> <h1>{0}</h1> </body> </html>", "Hello, World!");
            
            var s = new StringWriter();
           
            builder.WriteTo(s, HtmlEncoder.Default);
            
            await page.SetContentAsync(s.ToString());
            
            var pdfStream = page.PdfStreamAsync().Result;
            
            await browser.CloseAsync();
            
            return pdfStream;
        }
    }
}
