namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    public class PurchaseOrder
    {
        public int OrderNumber { get; set; }

        public int SupplierId { get; set; }

        public int OurQty { get; set; }

        public string DocumentType { get; set; }

        public Supplier Supplier { get; set; }
    }
}
