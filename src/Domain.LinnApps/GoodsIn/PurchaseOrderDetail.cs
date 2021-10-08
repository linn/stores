namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PurchaseOrderDetail
    {
        public int Line { get; set; }

        public int OrderNumber { get; set; }

        public int? OurQty { get; set; }

        public string PartNumber { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        public string RohsCompliant { get; set; }

        public SalesArticle SalesArticle { get; set; }

        public string SuppliersDesignation { get; set; }
    }
}
