namespace Linn.Stores.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.Stores.Resources.External;

    public class ProductsService : IProductsService
    {
        private readonly IRestClient restClient;

        private readonly string rootUri;

        public ProductsService(IRestClient restClient, string rootUri)
        {
            this.restClient = restClient;
            this.rootUri = rootUri;
        }

        public string GetLinkToProduct(string partNumber)
        {
            var uri = new Uri(
                $"{this.rootUri}/products/search?name={partNumber}"
                + "&filters=sales-product"
                + "&filters=sales-part" 
                + "&filters=service-part&showPhasedOut=true",
                UriKind.RelativeOrAbsolute);
            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            var json = new JsonSerializer();

            var resource = json.Deserialize<IEnumerable<ProductResource>>(response.Value);

            return resource.FirstOrDefault()?.Href;
        }
    }
}
