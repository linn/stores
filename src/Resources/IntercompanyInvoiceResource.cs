namespace Linn.Stores.Resources
{
    using System;
    public class IntercompanyInvoiceResource
    {
        public AddressResource DeliveryAddress { get; set; }
        public string DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public int RaisedBy { get; set; }
        public string ExportOrReturn { get; set; }
        public int AccountId { get; set; }
        public string DocumentReference { get; set; }
        public decimal NetTotal { get; set; }
        public decimal VATTotal { get; set; }
        public decimal DocumentTotal { get; set; }
        public string Terms { get; set; }
        public int NumPallets { get; set; }
        public int NumCartons { get; set; }
        public decimal GrossWeightKg { get; set; }
        public decimal GrossDimsM3 { get; set; }
        public string ToAccountingCompany { get; set; }
        public int? ConsignmentId { get; set; }
        public AddressResource InvoiceAddress { get; set; }
        public string CustomStamp { get; set; }
        public string Currency { get; set; }
        public string CustomerVATRegNo { get; set; }
        public string InvoiceDeclaration { get; set; }
        public int ExportReturnId { get; set; }
    }
}