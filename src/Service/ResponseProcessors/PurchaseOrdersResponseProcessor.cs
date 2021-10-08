namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class PurchaseOrdersResponseProcessor : JsonResponseProcessor<IEnumerable<PurchaseOrder>>
    {
        public PurchaseOrdersResponseProcessor(IResourceBuilder<IEnumerable<PurchaseOrder>> resourceBuilder)
            : base(resourceBuilder, "purchase-orders", 1)
        {
        }
    }
}
