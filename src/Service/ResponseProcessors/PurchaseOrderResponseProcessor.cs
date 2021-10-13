namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class PurchaseOrderResponseProcessor : JsonResponseProcessor<PurchaseOrder>
    {
        public PurchaseOrderResponseProcessor(IResourceBuilder<PurchaseOrder> resourceBuilder)
            : base(resourceBuilder, "purchase-orders", 1)
        {
        }
    }
}
