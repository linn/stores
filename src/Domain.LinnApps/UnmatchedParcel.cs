namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class UnmatchedParcel
    {
        public int ParcelNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateReceived { get; set; }

        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string CarrierName { get; set; }

        public string CheckedBy { get; set; }

        public string Comments { get; set; }

        public string ConsignmentNumber { get; set; }
    }
}
