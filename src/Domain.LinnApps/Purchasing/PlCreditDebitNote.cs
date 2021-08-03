﻿namespace Linn.Stores.Domain.LinnApps.Purchasing
{
    using System;

    public class PlCreditDebitNote
    {
        public int NoteNumber { get; set; }

        public string NoteType { get; set; }

        public string PartNumber { get; set; }

        public int OrderQty { get; set; }

        public int? OriginalOrderNumber { get; set; }

        public int? ReturnsOrderNumber { get; set; }

        public decimal NetTotal { get; set; }

        public string Notes { get; set; }

        public DateTime? DateClosed { get; set; }

        public int? ClosedBy { get; set; }

        public string ReasonClosed { get; set; }

        public int? SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}