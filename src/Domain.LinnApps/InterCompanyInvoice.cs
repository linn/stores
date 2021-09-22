using System.Text;

namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class InterCompanyInvoice
    {
        public string DocumentType { get; set; }

        public int DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public int ExportReturnId { get; set; }

        public int InvoiceAddressId { get; set; }

        public int SalesAccount { get; set; }

        public Address InvoiceAddress { get; set; }

        public int DeliveryAddressId { get; set; }

        public Address DeliveryAddress { get; set; }

        public decimal NetTotal { get; set; }

        public decimal VATTotal { get; set; }

        public decimal DocumentTotal { get; set; }

        public string CurrencyCode { get; set; }

        public Currency Currency { get; set; }

        public decimal GrossWeightKG { get; set; }

        public decimal GrossDimsM3 { get; set; }

        public string Terms { get; set; }

        public int? ConsignmentId { get; set; }
    }
}
