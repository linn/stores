namespace Linn.Stores.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Resources.External;

    public class ProductionTriggerLevelsProxy : IProductionTriggerLevelsService
    {
        private readonly IRestClient restClient;

        private readonly string rootUri;

        public ProductionTriggerLevelsProxy(IRestClient restClient, string rootUri)
        {
            this.restClient = restClient;
            this.rootUri = rootUri;
        }

        public string GetWorkStationCode(string partNumber)
        {
            // http://localhost:51690/
            // var uri = new Uri(
            // $"{this.rootUri}/production/maintenance/production-trigger-levels/{partNumber}",
            //     UriKind.RelativeOrAbsolute);

            var uri = new Uri(
                $"http://localhost:51690/production/maintenance/production-trigger-levels/{partNumber}",
                UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            var json = new JsonSerializer();

            var resource = json.Deserialize<ProductionTriggerLevelResource>(response.Value);

            return resource.WorkStationName;
        }
    }
}
