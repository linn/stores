namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class ExportReturnDetail
    {
        public int ReturnId { get; set; }
        
        public int RsnNumber { get; set; }
        
        public string ArticleNumber { get; set; }
        
        public int LineNo { get; set; }
        
        public int Qty { get; set; }
        
        public string Description { get; set; }
        
        public decimal? CustomsValue { get; set; }
        
        public decimal? BaseCustomsValue { get; set; }
        
        public int TariffId { get; set; }
        
        public string ExpinvDocumentType { get; set; }
        
        public int? ExpinvDocumentNumber { get; set; }

        public DateTime? ExpinvDate { get; set; }

        public int? NumCartons { get; set; }
        
        public double? Weight { get; set; }
        
        public double? Width { get; set; }
        
        public double? Height { get; set; }
        
        public double? Depth { get; set; }

        public ExportReturn ExportReturn { get; set; }
    }
}