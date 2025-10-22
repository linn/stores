namespace Linn.Stores.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Linn.Common.Proxy;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class PrintService : IPrintService
    {
        private readonly IRestClient restClient;
        private readonly string rootUri;

        public PrintService(IRestClient restClient, string rootUri)
        {
            this.restClient = restClient;
            this.rootUri = rootUri;
        }

        public async Task PrintDocument(
            string printerUri,
            string documentType,
            int documentNumber,
            bool showTermsAndConditions,
            bool showPrices)
        {
            var uri = new Uri($"{this.rootUri}/sales/documents/print", UriKind.RelativeOrAbsolute);

            var resource = new
                               {
                                   PrinterUri = printerUri,
                                   DocumentType = documentType,
                                   DocumentNumber = documentNumber,
                                   ShowTermsAndConditions = showTermsAndConditions,
                                   ShowPrices = showPrices
                               };

            var response = await this.restClient.Post<object>(
                               CancellationToken.None,
                               uri,
                               new Dictionary<string, string>(),
                               new Dictionary<string, string[]>
                                   {
                                       { "Accept", new[] { "application/json" } }
                                   },
                               resource);

            if (response == null || response.Value == null)
            {
                throw new PrintServiceException("Print proxy returned no data.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new PrintServiceException(
                    $"Print proxy failed: HTTP {(int)response.StatusCode}");
            }
        }
    }
}
