namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class PurchaseOrderResourceBuilder : IResourceBuilder<PurchaseOrder>
    {
        public PurchaseOrderResource Build(PurchaseOrder purchaseOrder)
        {
            return new PurchaseOrderResource
                       {
                           OrderNumber = purchaseOrder.OrderNumber,
                           SupplierId = purchaseOrder.SupplierId,
                           SuppliersDesignation = purchaseOrder.Details.First().SuppliersDesignation,
                           TariffCode = purchaseOrder.Details.First().SalesArticle.Tariff.TariffCode,
                           LineNumber = 1,
                           Links = this.BuildLinks(purchaseOrder).ToArray()
                       };
        }

        public string GetLocation(PurchaseOrder p)
        {
            return $"/logistics/purchase-orders/{p.OrderNumber}";
        }

        object IResourceBuilder<PurchaseOrder>.Build(PurchaseOrder purchaseOrder)
        {
            return this.Build(purchaseOrder);
        }

        private IEnumerable<LinkResource> BuildLinks(PurchaseOrder purchaseOrder)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(purchaseOrder) };
        }
    }
}
