namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;

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

        public int? FreightCharges { get; set; }

        public int? HandlingCharge { get; set; }

        public int? ClearanceCharge { get; set; }

        public int? Cartage { get; set; }

        public int? Duty { get; set; }

        public int? Vat { get; set; }

        public int? Misc { get; set; }

        public int? CarriersInvTotal { get; set; }

        public int? CarriersVatTotal { get; set; }

        public int TotalImportValue { get; set; }

        public int? Pieces { get; set; }

        public int? Weight { get; set; }

        public string CustomsEntryCode { get; set; }

        public DateTime? CustomsEntryCodeDate { get; set; }

        public int? LinnDuty { get; set; }

        public int? LinnVat { get; set; }

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

        public int? Storage { get; set; }

        public int? NumCartons { get; set; }

        public int? NumPallets { get; set; }

        public string Comments { get; set; }

        public int? ExchangeRate { get; set; }

        public string ExchangeCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public int? PeriodNumber { get; set; }

        public int? CreatedBy { get; set; }

        public string PortCode { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public ImpBookOrderDetail OrderDetail { get; set; }
        
        public ImpBookPostEntry PostEntry { get; set; }
        
        public ImpBookInvoiceDetail InvoiceDetail { get; set; }
    }
}
