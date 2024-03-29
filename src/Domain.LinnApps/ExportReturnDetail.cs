﻿namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
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
        
        public string ExpInvDocumentType { get; set; }
        
        public int? ExpInvDocumentNumber { get; set; }

        public DateTime? ExpInvDate { get; set; }

        public int? NumCartons { get; set; }
        
        public double? Weight { get; set; }
        
        public double? Width { get; set; }
        
        public double? Height { get; set; }
        
        public double? Depth { get; set; }

        public ExportReturn ExportReturn { get; set; }

        public InterCompanyInvoice InterCompanyInvoice { get; set; }
    }
}
