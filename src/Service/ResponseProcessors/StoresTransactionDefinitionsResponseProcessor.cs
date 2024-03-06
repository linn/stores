namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class StoresTransactionDefinitionsResponseProcessor : JsonResponseProcessor<IEnumerable<StoresTransactionDefinition>>
    {
        public StoresTransactionDefinitionsResponseProcessor(IResourceBuilder<IEnumerable<StoresTransactionDefinition>> resourceBuilder)
            : base(resourceBuilder, "linnapps-stores-transaction-definition", 1)
        {
        }
    }
}
