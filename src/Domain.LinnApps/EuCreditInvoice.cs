namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class EuCreditInvoice
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public int Invoice { get; set; }

        public int LineNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal GoodsTotal { get; set; }

        public decimal VatTotal { get; set; }

        public decimal DocumentTotal { get; set; }

        public int RsnNumber { get; set; }

        public int CreditNoteNumber { get; set; }

        public string DocumentType { get; set; }

        public string CreditCode { get; set; }

        public string CreditCodeDescription { get; set; }
    }
}
