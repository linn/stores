namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ImportBookService : IImportBookService
    {
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

        private void UpdateInvoiceDetails(IList<ImportBookInvoiceDetail> from, IList<ImportBookInvoiceDetail> to)
        {
            if (!from.Any() && !to.Any())
            {
                return;
            }

            //need to do something else for these, can't modify the list while I'm enumerating on it
            foreach (var oldDetail in from)
            {
                if (!to.Any(x => oldDetail.LineNumber == x.LineNumber))
                {
                    from.Remove(oldDetail);
                }
            }

            foreach (var newdetail in to)
            {
                
                var currentDetail = from.Any()
                                        ? from.FirstOrDefault(x => x.LineNumber == newdetail.LineNumber)
                                        : null;

                if (currentDetail == null)
                {
                    from.Add(newdetail);
                }
                else
                {
                    currentDetail.InvoiceNumber = newdetail.InvoiceNumber;
                    currentDetail.InvoiceValue = newdetail.InvoiceValue;
                }
            }
        }

        private void UpdateOrderDetails(IList<ImportBookOrderDetail> from, IList<ImportBookOrderDetail> to)
        {
            if (!from.Any() && !to.Any())
            {
                return;
            }

            foreach (var oldDetail in from)
            {
                if (!to.Any(x => oldDetail.LineNumber == x.LineNumber))
                {
                    from.Remove(oldDetail);
                }
            }

            foreach (var newdetail in to)
            {

                var currentDetail = from.Any()
                                        ? from.FirstOrDefault(x => x.LineNumber == newdetail.LineNumber)
                                        : null;

                if (currentDetail == null)
                {
                    from.Add(newdetail);
                }
                else
                {
                    currentDetail.OrderNumber = newdetail.OrderNumber;
                    currentDetail.RsnNumber = newdetail.RsnNumber;
                    currentDetail.OrderDescription = newdetail.OrderDescription;
                    currentDetail.Qty = newdetail.Qty;
                    currentDetail.DutyValue = newdetail.DutyValue;
                    currentDetail.FreightValue = newdetail.FreightValue;
                    currentDetail.VatValue = newdetail.VatValue;
                    currentDetail.OrderValue = newdetail.OrderValue;
                    currentDetail.Weight = newdetail.Weight;
                    currentDetail.LoanNumber = newdetail.LoanNumber;
                    currentDetail.LineType = newdetail.LineType;
                    currentDetail.CpcNumber = newdetail.CpcNumber;
                    currentDetail.TariffCode = newdetail.TariffCode;
                    currentDetail.InsNumber = newdetail.InsNumber;
                    currentDetail.VatRate = newdetail.VatRate;
                }
            }
        }

        private void UpdatePostEntries(IList<ImportBookPostEntry> from, IList<ImportBookPostEntry> to)
        {
            if (!from.Any() && !to.Any())
            {
                return;
            }

            foreach (var oldEntry in from)
            {
                if (!to.Any(x => oldEntry.LineNumber == x.LineNumber))
                {
                    from.Remove(oldEntry);
                }
            }

            foreach (var newEntry in to)
            {

                var currentEntry = from.Any()
                                       ? from.FirstOrDefault(x => x.LineNumber == newEntry.LineNumber)
                                       : null;

                if (currentEntry == null)
                {
                    from.Add(newEntry);
                }
                else
                {
                    currentEntry.EntryCodePrefix = newEntry.EntryCodePrefix;
                    currentEntry.EntryCode = newEntry.EntryCode;
                    currentEntry.EntryDate = newEntry.EntryDate;
                    currentEntry.Reference = newEntry.Reference;
                    currentEntry.Duty = newEntry.Duty;
                    currentEntry.Vat = newEntry.Vat;
                }
            }
        }
    }
}
