namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class PurchaseOrdersResourceBuilder : IResourceBuilder<IEnumerable<PurchaseOrder>>
    {
        private readonly PurchaseOrderResourceBuilder purchaseOrderResourceBuilder = new PurchaseOrderResourceBuilder();

        public IEnumerable<PurchaseOrderResource> Build(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.OrderBy(b => b.OrderNumber)
                .Select(a => this.purchaseOrderResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<PurchaseOrder>>.Build(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            return this.Build(purchaseOrders);
        }
    }
}
