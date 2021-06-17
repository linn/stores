namespace Linn.Stores.Proxy
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    public class PdfService : IPdfService
    {
        private readonly string htmlToPdfConverterServiceUrl;

        public PdfService(string htmlToPdfConverterServiceUrl)
        {
            this.htmlToPdfConverterServiceUrl = htmlToPdfConverterServiceUrl;
        }

        public async Task<Stream> ConvertHtmlToPdf(string html, bool landscape)
        {
            using (var client = new HttpClient())
            {
                using (var multiPartStream = new MultipartFormDataContent())
                {
                    var bytes = Encoding.UTF8.GetBytes(html);
                    multiPartStream.Add(new StringContent("{}"), "metadata");
                    multiPartStream.Add(new ByteArrayContent(bytes, 0, bytes.Length), "files", "file.html");
                    var request =
                        new HttpRequestMessage(HttpMethod.Post, this.htmlToPdfConverterServiceUrl)
                            {
                                Content = multiPartStream
                            };

                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStreamAsync();
                        return res;
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
