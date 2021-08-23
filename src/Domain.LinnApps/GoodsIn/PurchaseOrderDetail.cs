namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    public class PurchaseOrderDetail
    {
        public int OrderNumber { get; set; }

        public int Line { get; set; }

        public string RohsCompliant { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
