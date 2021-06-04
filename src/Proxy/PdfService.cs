namespace Linn.Stores.Proxy
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using PuppeteerSharp;
    using PuppeteerSharp.Media;

    public class PdfService : IPdfService
    {
        private readonly Browser browser;

        public PdfService(Browser browser)
        {
            this.browser = browser;
        }

        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape, bool showPageNumbers)
        {
            var page = await this.browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions 
                                 { 
                                    Landscape = landscape, 
                                    DisplayHeaderFooter = true,
                                    FooterTemplate = showPageNumbers ? 
                                                         "<div style=\"text-align: right;width: 297mm;font-size: 8px;\"><span style=\"margin-right: 1cm\"><span class=\"pageNumber\"></span> of <span class=\"totalPages\"></span></span></div>"
                                                         : string.Empty,
                                    MarginOptions = showPageNumbers ? new MarginOptions { Bottom = "40" } : null
                                 };

            var pdfStream = page.PdfStreamAsync(pdfOptions).Result;

            await page.CloseAsync();

            return pdfStream;
        }
    }
}
