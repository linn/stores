namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class PurchaseOrderResource : HypermediaResource
    {
        public int LineNumber { get; set; }

        public int OrderNumber { get; set; }

        public int SupplierId { get; set; }

        public string SuppliersDesignation { get; set; }

        public string TariffCode { get; set; }
    }
}
