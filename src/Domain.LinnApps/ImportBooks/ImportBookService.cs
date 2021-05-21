namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ImportBookService : IImportBookService
    {
        private readonly IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey> InvoiceDetailRepository;

        private readonly IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> OrderDetailRepository;

        private readonly IRepository<ImportBookPostEntry, ImportBookPostEntryKey> PostEntryRepository;

        public ImportBookService(
            IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey> invoiceDetailRepository,
            IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> orderDetailRepository,
            IRepository<ImportBookPostEntry, ImportBookPostEntryKey> postEntryRepository)
        {
            this.InvoiceDetailRepository = invoiceDetailRepository;
            this.OrderDetailRepository = orderDetailRepository;
            this.PostEntryRepository = postEntryRepository;
        }

        public void Update(
            ImportBook from,
            ImportBook to)
        {
            this.UpdateTopLevelProperties(from, to);

            this.UpdateInvoiceDetails(from.InvoiceDetails, to.InvoiceDetails);

            this.UpdateOrderDetails(from.OrderDetails, to.OrderDetails);

            this.UpdatePostEntries(from.PostEntries, to.PostEntries);
        }

        private void UpdateTopLevelProperties(ImportBook entity, ImportBook to)
        {
            entity.DateCreated = to.DateCreated;
            entity.ParcelNumber = to.ParcelNumber;
            entity.SupplierId = to.SupplierId;
            entity.ForeignCurrency = to.ForeignCurrency;
            entity.Currency = to.Currency;
            entity.CarrierId = to.CarrierId;
            entity.OldArrivalPort = to.OldArrivalPort;
            entity.FlightNumber = to.FlightNumber;
            entity.TransportId = to.TransportId;
            entity.TransportBillNumber = to.TransportBillNumber;
            entity.TransactionId = to.TransactionId;
            entity.DeliveryTermCode = to.DeliveryTermCode;
            entity.ArrivalPort = to.ArrivalPort;
            entity.LineVatTotal = to.LineVatTotal;
            entity.Hwb = to.Hwb;
            entity.SupplierCostCurrency = to.SupplierCostCurrency;
            entity.TransNature = to.TransNature;
            entity.ArrivalDate = to.ArrivalDate;
            entity.FreightCharges = to.FreightCharges;
            entity.HandlingCharge = to.HandlingCharge;
            entity.ClearanceCharge = to.ClearanceCharge;
            entity.Cartage = to.Cartage;
            entity.Duty = to.Duty;
            entity.Vat = to.Vat;
            entity.Misc = to.Misc;
            entity.CarriersInvTotal = to.CarriersInvTotal;
            entity.CarriersVatTotal = to.CarriersVatTotal;
            entity.TotalImportValue = to.TotalImportValue;
            entity.Pieces = to.Pieces;
            entity.Weight = to.Weight;
            entity.CustomsEntryCode = to.CustomsEntryCode;
            entity.CustomsEntryCodeDate = to.CustomsEntryCodeDate;
            entity.LinnDuty = to.LinnDuty;
            entity.LinnVat = to.LinnVat;
            entity.IprCpcNumber = to.IprCpcNumber;
            entity.EecgNumber = to.EecgNumber;
            entity.DateCancelled = to.DateCancelled;
            entity.CancelledBy = to.CancelledBy;
            entity.CancelledReason = to.CancelledReason;
            entity.CarrierInvNumber = to.CarrierInvNumber;
            entity.CarrierInvDate = to.CarrierInvDate;
            entity.CountryOfOrigin = to.CountryOfOrigin;
            entity.FcName = to.FcName;
            entity.VaxRef = to.VaxRef;
            entity.Storage = to.Storage;
            entity.NumCartons = to.NumCartons;
            entity.NumPallets = to.NumPallets;
            entity.Comments = to.Comments;
            entity.ExchangeRate = to.ExchangeRate;
            entity.ExchangeCurrency = to.ExchangeCurrency;
            entity.BaseCurrency = to.BaseCurrency;
            entity.PeriodNumber = to.PeriodNumber;
            entity.CreatedBy = to.CreatedBy;
            entity.PortCode = to.PortCode;
            entity.CustomsEntryCodePrefix = to.CustomsEntryCodePrefix;
        }

        private void UpdateInvoiceDetails(IEnumerable<ImportBookInvoiceDetail> from, IEnumerable<ImportBookInvoiceDetail> to)
        {
            foreach (var detail in to)
            {

                var currentDetail = from.Any()
                                        ? from.FirstOrDefault(
                                            x => x.ImportBookId == detail.ImportBookId
                                                 && x.LineNumber == detail.LineNumber)
                                        : null;

                if (currentDetail ==  null)
                {
                    this.CreateInvoiceDetail(detail);
                }
                else
                {
                    currentDetail.InvoiceNumber = detail.InvoiceNumber;
                    currentDetail.InvoiceValue = detail.InvoiceValue;
                }
            }
        }

        private void UpdateOrderDetails(IEnumerable<ImportBookOrderDetail> from, IEnumerable<ImportBookOrderDetail> to)
        {
            foreach (var detail in to)
            {
                var currentDetail = from.Any()
                                        ? from.FirstOrDefault(
                                            x => x.ImportBookId == detail.ImportBookId
                                                 && x.LineNumber == detail.LineNumber)
                                        : null;
                if (currentDetail == null)
                {
                    this.CreateOrderDetail(detail);
                }
                else
                {
                    currentDetail.OrderNumber = detail.OrderNumber;
                    currentDetail.RsnNumber = detail.RsnNumber;
                    currentDetail.OrderDescription = detail.OrderDescription;
                    currentDetail.Qty = detail.Qty;
                    currentDetail.DutyValue = detail.DutyValue;
                    currentDetail.FreightValue = detail.FreightValue;
                    currentDetail.VatValue = detail.VatValue;
                    currentDetail.OrderValue = detail.OrderValue;
                    currentDetail.Weight = detail.Weight;
                    currentDetail.LoanNumber = detail.LoanNumber;
                    currentDetail.LineType = detail.LineType;
                    currentDetail.CpcNumber = detail.CpcNumber;
                    currentDetail.TariffCode = detail.TariffCode;
                    currentDetail.InsNumber = detail.InsNumber;
                    currentDetail.VatRate = detail.VatRate;
                }
            }
        }

        private void UpdatePostEntries(IEnumerable<ImportBookPostEntry> from, IEnumerable<ImportBookPostEntry> to)
        {
            foreach (var entry in to)
            {
                var currentEntry = from.Any()
                                       ? from.FirstOrDefault(
                                           x => x.ImportBookId == entry.ImportBookId
                                                && x.LineNumber == entry.LineNumber)
                                       : null;
                if (currentEntry == null)
                {
                    this.CreatePostEntry(entry);
                }
                else
                {
                    currentEntry.EntryCodePrefix = entry.EntryCodePrefix;
                    currentEntry.EntryCode = entry.EntryCode;
                    currentEntry.EntryDate = entry.EntryDate;
                    currentEntry.Reference = entry.Reference;
                    currentEntry.Duty = entry.Duty;
                    currentEntry.Vat = entry.Vat;
                }
            }
        }

        private void CreateInvoiceDetail(ImportBookInvoiceDetail detail)
        {
            this.InvoiceDetailRepository.Add(detail);
        }

        private void CreateOrderDetail(ImportBookOrderDetail detail)
        {
            this.OrderDetailRepository.Add(detail);
        }

        private void CreatePostEntry(ImportBookPostEntry entry)
        {
            this.PostEntryRepository.Add(entry);
        }
    }
}
