namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class SalesOutletsResponseProcessor : JsonResponseProcessor<IEnumerable<SalesOutlet>>
    {
        public SalesOutletsResponseProcessor(IResourceBuilder<IEnumerable<SalesOutlet>> resourceBuilder)
            : base(resourceBuilder, "sales-outlets", 1)
        {
        }
    }
}