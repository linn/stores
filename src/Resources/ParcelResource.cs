namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class ParcelResource : HypermediaResource
    {
        public int ParcelNumber { get; set; }

        public int? SupplierId { get; set; }

        public string DateCreated { get; set; }

        public int? CarrierId { get; set; }

        public string SupplierInvoiceNo { get; set; }

        public string ConsignmentNo { get; set; }

        public int? CartonCount { get; set; }

        public int? PalletCount { get; set; }

        public decimal Weight { get; set; }

        public string DateReceived { get; set; }

        public int CheckedById { get; set; }

        public string Comments { get; set; }

        public string DateCancelled { get; set; }

        public int? CancelledBy { get; set; }

        public string CancellationReason { get; set; }
    }
}
