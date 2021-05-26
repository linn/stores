namespace Linn.Stores.Service.Host
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;

    using PuppeteerSharp;

    public static class PuppeteerExtensions
    {
        private static string executablePath;

        public static string ExecutablePath => executablePath;

        public static async Task PreparePuppeteerAsync(
            this IApplicationBuilder applicationBuilder,
            IHostingEnvironment hostingEnvironment)
        {
            var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, @"\puppeteer");
            var browserOptions = new BrowserFetcherOptions { Path = downloadPath };
            var browserFetcher = new BrowserFetcher(browserOptions);
            executablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
        }
    }
}
