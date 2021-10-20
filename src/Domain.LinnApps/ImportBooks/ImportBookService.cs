namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Persistence;

    public class ImportBookService : IImportBookService
    {
        private readonly IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> exchangeRateRepository;
        private readonly IRepository<LedgerPeriod, int> ledgerPeriodRepository;

        public ImportBookService(
            IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> exchangeRateRepository,
            IRepository<LedgerPeriod, int> ledgerPeriodRepository)
        {
            this.exchangeRateRepository = exchangeRateRepository;
            this.ledgerPeriodRepository = ledgerPeriodRepository;
        }

        public void Update(ImportBook from, ImportBook to)
        {
            this.UpdateTopLevelProperties(from, to);

            this.UpdateInvoiceDetails(from.InvoiceDetails, to.InvoiceDetails);

            this.UpdateOrderDetails(from.OrderDetails, to.OrderDetails);

            this.UpdatePostEntries(from.PostEntries, to.PostEntries);
        }

        public IEnumerable<ImportBookExchangeRate> GetExchangeRates(string date)
        {
            var unformattedDate = DateTime.Parse(date);
            var formattedDate = unformattedDate.ToString("MMMYYYY");

            var ledgerPeriod = this.ledgerPeriodRepository.FindBy(x => x.MonthName == formattedDate);
            
            var exchangeRates = this.exchangeRateRepository.FilterBy(x => x.PeriodNumber == ledgerPeriod.PeriodNumber);

            return exchangeRates;
        }

        private void UpdateTopLevelProperties(ImportBook entity, ImportBook to)
        {
            entity.ParcelNumber = to.ParcelNumber;
            entity.SupplierId = to.SupplierId;
            entity.ForeignCurrency = to.ForeignCurrency;
            entity.Currency = to.Currency;
            entity.CarrierId = to.CarrierId;
            entity.TransportId = to.TransportId;
            entity.TransportBillNumber = to.TransportBillNumber;
            entity.TransactionId = to.TransactionId;
            entity.DeliveryTermCode = to.DeliveryTermCode;
            entity.ArrivalPort = to.ArrivalPort;
            entity.ArrivalDate = to.ArrivalDate;
            entity.TotalImportValue = to.TotalImportValue;
            entity.Weight = to.Weight;
            entity.CustomsEntryCode = to.CustomsEntryCode;
            entity.CustomsEntryCodeDate = to.CustomsEntryCodeDate;
            entity.LinnDuty = to.LinnDuty;
            entity.LinnVat = to.LinnVat;
            entity.DateCancelled = to.DateCancelled;
            entity.CancelledBy = to.CancelledBy;
            entity.CancelledReason = to.CancelledReason;
            entity.NumCartons = to.NumCartons;
            entity.NumPallets = to.NumPallets;
            entity.Comments = to.Comments;
            entity.CreatedBy = to.CreatedBy;
            entity.CustomsEntryCodePrefix = to.CustomsEntryCodePrefix;
            entity.Pva = to.Pva;
        }

        private void UpdateInvoiceDetails(IList<ImportBookInvoiceDetail> from, IList<ImportBookInvoiceDetail> to)
        {
            if (!from.Any() && !to.Any())
            {
                return;
            }

            foreach (var newdetail in to)
            {
                var currentDetail = from.Any() ? from.FirstOrDefault(x => x.LineNumber == newdetail.LineNumber) : null;

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

            foreach (var newdetail in to)
            {
                var currentDetail = from.Any() ? from.FirstOrDefault(x => x.LineNumber == newdetail.LineNumber) : null;

                if (currentDetail == null)
                {
                    //below hard coded line number as one may change eventually
                    //if more than one purchase order detail is implemented for a purchase order
                    newdetail.POLineNumber = 1;
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

            foreach (var newEntry in to)
            {
                var currentEntry = from.Any() ? from.FirstOrDefault(x => x.LineNumber == newEntry.LineNumber) : null;

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
