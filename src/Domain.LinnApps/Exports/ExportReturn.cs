namespace Linn.Stores.Domain.LinnApps.Exports
{
    using System;
    using System.Collections.Generic;

    public class ExportReturn
    {
        public int ReturnId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateDispatched { get; set; }

        public DateTime? DateCancelled { get; set; }

        public Carrier Carrier { get; set; }

        public Hub Hub { get; set; }

        public string Currency { get; set; }

        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public SalesOutlet SalesOutlet { get; set; }

        public string CarrierRef { get; set; }

        public string Terms { get; set; }

        public int? NumPallets { get; set; }

        public int? NumCartons { get; set; }

        public decimal? GrossWeightKg { get; set; }

        public decimal? GrossDimsM3 { get; set; }

        public int InterCompany_Doc_Number { get; set; }

        public Employee RaisedBy { get; set; }

        public IEnumerable<ExportReturnDetail> Details { get; set; }
    }
}

