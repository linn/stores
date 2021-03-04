namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class SalesAccountsResponseProcessor : JsonResponseProcessor<IEnumerable<SalesAccount>>
    {
        public SalesAccountsResponseProcessor(IResourceBuilder<IEnumerable<SalesAccount>> resourceBuilder)
            : base(resourceBuilder, "sales-accounts", 1)
        {
        }
    }
}