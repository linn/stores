namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;

    public class ImportBook
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public int? ParcelNumber { get; set; }

        public int SupplierId { get; set; }

        public string ForeignCurrency { get; set; }

        public string Currency { get; set; }

        public int CarrierId { get; set; }

        public string OldArrivalPort { get; set; }

        public string FlightNumber { get; set; }

        public int TransportId { get; set; }

        public string TransportBillNumber { get; set; }

        public int TransactionId { get; set; }

        public string DeliveryTermCode { get; set; }

        public string ArrivalPort { get; set; }

        public int? LineVatTotal { get; set; }

        public string Hwb { get; set; }

        public string SupplierCostCurrency { get; set; }

        public string TransNature { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public decimal? FreightCharges { get; set; }

        public decimal? HandlingCharge { get; set; }

        public decimal? ClearanceCharge { get; set; }

        public decimal? Cartage { get; set; }

        public decimal? Duty { get; set; }

        public decimal? Vat { get; set; }

        public decimal? Misc { get; set; }

        public decimal? CarriersInvTotal { get; set; }

        public decimal? CarriersVatTotal { get; set; }

        public decimal TotalImportValue { get; set; }

        public int? Pieces { get; set; }

        public decimal? Weight { get; set; }

        public string CustomsEntryCode { get; set; }

        public DateTime? CustomsEntryCodeDate { get; set; }

        public decimal? LinnDuty { get; set; }

        public decimal? LinnVat { get; set; }

        public int? IprCpcNumber { get; set; }

        public int? EecgNumber { get; set; }

        public DateTime? DateCancelled { get; set; }

        public int? CancelledBy { get; set; }

        public string CancelledReason { get; set; }

        public string CarrierInvNumber { get; set; }

        public DateTime? CarrierInvDate { get; set; }

        public string CountryOfOrigin { get; set; }

        public string FcName { get; set; }

        public string VaxRef { get; set; }

        public decimal? Storage { get; set; }

        public int? NumCartons { get; set; }

        public int? NumPallets { get; set; }

        public string Comments { get; set; }

        public decimal? ExchangeRate { get; set; }

        public string ExchangeCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public int? PeriodNumber { get; set; }

        public int? CreatedBy { get; set; }

        public string PortCode { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public IList<ImportBookInvoiceDetail> InvoiceDetails { get; set; }

        public IList<ImportBookOrderDetail> OrderDetails { get; set; }

        public IList<ImportBookPostEntry> PostEntries { get; set; }

    }
}
