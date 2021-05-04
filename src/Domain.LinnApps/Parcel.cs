namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class Parcel
    {
        public int ParcelNumber { get; set; }

        public int? SupplierId { get; set; }

        public DateTime DateCreated { get; set; }

        public int? CarrierId { get; set; }

        public string SupplierInvoiceNo { get; set; }

        public string ConsignmentNo { get; set; }

        public int? CartonCount { get; set; }

        public int? PalletCount { get; set; }

        public decimal Weight { get; set; }

        public DateTime DateReceived { get; set; }

        public int CheckedById { get; set; }

        public string Comments { get; set; }

        public DateTime? DateCancelled { get; set; }

        public int? CancelledBy { get; set; }

        public string CancellationReason { get; set; }

        public int? ImportBookNo { get; set; }
    }
}
