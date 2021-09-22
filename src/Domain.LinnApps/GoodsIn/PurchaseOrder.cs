namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System.Collections.Generic;

    public class PurchaseOrder
    {
        public int OrderNumber { get; set; }

        public int SupplierId { get; set; }

        public string DocumentType { get; set; }

        public Supplier Supplier { get; set; }

        public IEnumerable<PurchaseOrderDetail> Details { get; set; }
    }
}
