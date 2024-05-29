namespace Linn.Stores.Proxy
{
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
                    var landscapeString = landscape ? "true" : "false";
                    var bytes = Encoding.UTF8.GetBytes(html);
                    
                    multiPartStream.Add(
                        new ByteArrayContent(bytes, 0, bytes.Length), 
                        "files", 
                        "index.html");
                    
                    multiPartStream.Add(new StringContent(landscapeString), "landscape");
                    
                    var request =
                        new HttpRequestMessage(HttpMethod.Post, this.htmlToPdfConverterServiceUrl + "/forms/chromium/convert/html")
                            {
                                Content = multiPartStream
                            };

                    var response = await client.SendAsync(
                                       request, 
                                       HttpCompletionOption.ResponseContentRead);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new PdfServiceException(
                            "Pdf generation failed in API call: " 
                            + response.StatusCode + " - " 
                            + response.ReasonPhrase);
                    }

                    var res = await response.Content.ReadAsStreamAsync();
                    return res;
                }
            }
        }
    }
}
