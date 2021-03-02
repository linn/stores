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
        
        public int HubId { get; set; }
        
        public int? OutletNumber { get; set; }
        
        public DateTime? DateDispatched { get; set; }
        
        public DateTime? DateCancelled { get; set; }
        
        public string CarrierRef { get; set; }
        
        public string Terms { get; set; }
        
        public int? NumPallets { get; set; }
        
        public int? NumCartons { get; set; }
        
        public int? GrossWeightKg { get; set; }
        
        public int? GrossDimsM3 { get; set; }
        
        public int RaisedBy { get; set; }
        
        public string IntercoDocType { get; set; }
        
        public int? IntercoDocNumber { get; set; }

        public IEnumerable<ExportReturnDetail> ExportReturnDetails { get; set; }
    }
}