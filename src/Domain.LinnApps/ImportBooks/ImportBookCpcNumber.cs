namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;

    public class ImportBookCpcNumber
    {
        public int CpcNumber { get; set; }
        
        public string Description { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
