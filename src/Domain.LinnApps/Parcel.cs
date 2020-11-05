﻿namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class Parcel
    {
        public int ParcelNumber { get; set; }

        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string SupplierCountry { get; set; }

        public DateTime DateCreated { get; set; }

        public int CarrierId { get; set; }

        public string CarrierName { get; set; }

        public string SupplierInvoiceNo { get; set; }

        public int ConsignmentNo { get; set; }

        public int CartonCount { get; set; }

        public int PalletCount { get; set; }

        public decimal Weight { get; set; }

        public DateTime DateReceived { get; set; }

        public int CheckedById { get; set; }

        public string CheckedByName { get; set; }

        public string Comments { get; set; }

        public string ImportNoVax { get; set; }

        public int ImpbookId { get; set; }
    }
}
