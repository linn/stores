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
            builder.AppendHtmlLine(
                "<html><body><head><style>body{padding:20px;font-family:Helvetica;}.wrapper{display:grid;grid-template-columns:repeat(auto-fit,minmax(400px,1fr));grid-gap:10px;}.box{color:#000;border-radius:3px;padding:20px;font-size:18px;}</style></head><div class=\"wrapper\"><div class=\"boxa\">CONSIGNMENTNUMBER:{0}</div><div class=\"boxb\">B</div><div class=\"boxc\">C</div><div class=\"boxd\">D</div><div class=\"boxe\">E</div><div class=\"boxf\">F</div><div class=\"boxf\">F</div><div class=\"boxf\">F</div><div class=\"boxf\">F</div></div></body></html>");
            
            var s = new StringWriter();
           
            builder.WriteTo(s, HtmlEncoder.Default);

            await page.SetContentAsync(s.ToString());

            var pdfOptions = new PdfOptions { Landscape = true };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;
            
            await browser.CloseAsync();
            
            return pdfStream;
        }
    }
}
