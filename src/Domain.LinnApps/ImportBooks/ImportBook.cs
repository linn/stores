﻿namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;

    public class ImportBook
    {
        public DateTime? ArrivalDate { get; set; }

        public string ArrivalPort { get; set; }

        public string BaseCurrency { get; set; }

        public int? CancelledBy { get; set; }

        public string CancelledReason { get; set; }

        public Supplier Carrier { get; set; }

        public int CarrierId { get; set; }

        public string Comments { get; set; }

        public int? CreatedBy { get; set; }

        public string Currency { get; set; }

        public string CustomsEntryCode { get; set; }

        public DateTime? CustomsEntryCodeDate { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public DateTime? DateCancelled { get; set; }

        public DateTime DateCreated { get; set; }

        public string DeliveryTermCode { get; set; }

        public string ExchangeCurrency { get; set; }

        public decimal? ExchangeRate { get; set; }

        public string ForeignCurrency { get; set; }

        public int Id { get; set; }

        public IList<ImportBookInvoiceDetail> InvoiceDetails { get; set; }

        public decimal? LinnDuty { get; set; }

        public decimal? LinnVat { get; set; }

        public int? NumCartons { get; set; }

        public int? NumPallets { get; set; }

        public IList<ImportBookOrderDetail> OrderDetails { get; set; }

        public int? ParcelNumber { get; set; }

        public int? PeriodNumber { get; set; }

        public IList<ImportBookPostEntry> PostEntries { get; set; }

        public string Pva { get; set; }

        public Supplier Supplier { get; set; }

        public int SupplierId { get; set; }

        public decimal TotalImportValue { get; set; }

        public int TransactionId { get; set; }

        public string TransportBillNumber { get; set; }

        public int TransportId { get; set; }

        public decimal? Weight { get; set; }
    }
}
