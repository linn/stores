namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImportBookOrderDetail
    {
        public int ImportBookId { get; set; }
        
        public int LineNumber { get; set; }
        
        public int? OrderNumber { get; set; }
        
        public int? RsnNumber { get; set; }
        
        public string OrderDescription { get; set; }
        
        public int Qty { get; set; }
        
        public decimal DutyValue { get; set; }
        
        public decimal FreightValue { get; set; }
        
        public decimal VatValue { get; set; }
        
        public decimal OrderValue { get; set; }
        
        public decimal Weight { get; set; }
        
        public int? LoanNumber { get; set; }
        
        public string LineType { get; set; }
        
        public int? CpcNumber { get; set; }
        
        public string TariffCode { get; set; }
        
        public int? InsNumber { get; set; }
        
        public int? VatRate { get; set; }
    }
}