namespace Linn.Stores.Domain.LinnApps.InterCompanyInvoices
{
    using System;
    using System.Collections.Generic;

    public class InterCompanyInvoice
    {
        public string DocumentType { get; set; }

        public int DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public int ExportReturnId { get; set; }

        public int InvoiceAddressId { get; set; }

        public int SalesAccountId { get; set; }

        public Address InvoiceAddress { get; set; }

        public int DeliveryAddressId { get; set; }

        public Address DeliveryAddress { get; set; }

        public decimal NetTotal { get; set; }

        public decimal VATTotal { get; set; }

        public decimal DocumentTotal { get; set; }

        public string CurrencyCode { get; set; }

        public decimal GrossWeightKG { get; set; }

        public decimal GrossDimsM3 { get; set; }

        public string Terms { get; set; }

        public int? ConsignmentId { get; set; }

        public IList<InterCompanyInvoiceDetail> Details { get; set; }
    }
}
