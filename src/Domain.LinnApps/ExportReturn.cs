namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class ExportReturn
    {
        public string CarrierCode { get; set; }
        
        public int ReturnId { get; set; }

        public DateTime DateCreated { get; set; }
        
        public string Currency { get; set; }
        
        public int AccountId { get; set; }
        
        public int? HubId { get; set; }
        
        public int? OutletNumber { get; set; }
        
        public DateTime? DateDispatched { get; set; }
        
        public DateTime? DateCancelled { get; set; }
        
        public string CarrierRef { get; set; }
        
        public string Terms { get; set; }
        
        public int? NumPallets { get; set; }
        
        public int? NumCartons { get; set; }
        
        public double? GrossWeightKg { get; set; }
        
        public double? GrossDimsM3 { get; set; }

        public string MadeInterCompanyInvoices { get; set; }

        public DateTime? DateProcessed { get; set; }

        public string ReturnForCredit { get; set; }

        public string ExportCustomsEntryCode { get; set; }

        public DateTime? ExportCustomsCodeDate { get; set; }

        public Employee RaisedBy { get; set; }
        
        public SalesOutlet SalesOutlet { get; set; }

        public IEnumerable<ExportReturnDetail> ExportReturnDetails { get; set; }
    }
}
