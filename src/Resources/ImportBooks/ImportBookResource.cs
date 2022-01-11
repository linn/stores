namespace Linn.Stores.Resources.ImportBooks
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class ImportBookResource : HypermediaResource
    {
        public string ArrivalDate { get; set; }

        public string ArrivalPort { get; set; }

        public int? CancelledBy { get; set; }

        public string CancelledReason { get; set; }

        public int CarrierId { get; set; }

        public string Comments { get; set; }

        public int? CreatedBy { get; set; }

        public string Currency { get; set; }

        public string CustomsEntryCode { get; set; }

        public string CustomsEntryCodeDate { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public string DateCancelled { get; set; }

        public string DateCreated { get; set; }

        public string DeliveryTermCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public string ForeignCurrency { get; set; }

        public int Id { get; set; }

        public IEnumerable<ImportBookInvoiceDetailResource> ImportBookInvoiceDetails { get; set; }

        public IEnumerable<ImportBookOrderDetailResource> ImportBookOrderDetails { get; set; }

        public IEnumerable<ImportBookPostEntryResource> ImportBookPostEntries { get; set; }

        public decimal? LinnDuty { get; set; }

        public decimal? LinnVat { get; set; }

        public int? NumCartons { get; set; }

        public int? NumPallets { get; set; }

        public int? ParcelNumber { get; set; }

        public string Pva { get; set; }

        public int SupplierId { get; set; }

        public decimal TotalImportValue { get; set; }

        public int TransactionId { get; set; }

        public string TransportBillNumber { get; set; }

        public int TransportId { get; set; }

        public decimal? Weight { get; set; }
    }
}
