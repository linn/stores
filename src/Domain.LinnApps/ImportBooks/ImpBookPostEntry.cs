namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;

    public class ImpBookPostEntry
    {
        public int ImpBookId { get; set; }
        
        public int LineNumber { get; set; }
        
        public string EntryCodePrefix { get; set; }
        
        public string EntryCode { get; set; }
        
        public DateTime? EntryDate { get; set; }
        
        public string Reference { get; set; }
        
        public int? Duty { get; set; }
        
        public int? Vat { get; set; }
    }
}