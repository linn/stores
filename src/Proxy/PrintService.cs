namespace Linn.Stores.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.Stores.Domain.LinnApps;

    public class PrintService : IPrintService
    {
        private readonly IRestClient restClient;
        private readonly string rootUri;

        public PrintService(IRestClient restClient, string rootUri)
        {
            this.restClient = restClient;
            this.rootUri = rootUri;
        }

        public async Task<PrintResult> PrintDocument(
            string printerUri,
            string documentType,
            int documentNumber,
            bool showTermsAndConditions,
            bool showPrices)
        {
            var json = new JsonSerializer();
            var uri = new Uri($"{this.rootUri}/sales/documents/print", UriKind.RelativeOrAbsolute);

            var requestBody = json.Serialize(new
            {
                PrinterUri = printerUri,
                DocumentType = documentType,
                DocumentNumber = documentNumber,
                ShowTermsAndConditions = showTermsAndConditions,
                ShowPrices = showPrices
            });

            var response = await this.restClient.Post(
                               CancellationToken.None,
                               uri,
                               new Dictionary<string, string>(), // no query params
                               new Dictionary<string, string[]>
                                   {
                                       { "Content-Type", new[] { "application/json" } },
                                       { "Accept", new[] { "application/json" } }
                                   },
                               requestBody,
                               "application/json");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new PrintServiceException(
                    $"Print proxy failed: HTTP {(int)response.StatusCode}");
            }

            var result = json.Deserialize<PrintResult>(response.Value);
            if (result == null)
            {
                throw new PrintServiceException("Print proxy returned no data.");
            }

            return result;
        }
    }
}
