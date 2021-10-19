namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    public class ImportBookService : IImportBookService
    {
        private readonly IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> exchangeRateRepository;

        private readonly IRepository<LedgerPeriod, int> ledgerPeriodRepository;

        private readonly IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> orderDetailRepository;

        private readonly IPurchaseLedgerPack purchaseLedgerPack;

        private readonly IRepository<Supplier, int> supplierRepository;

        private readonly ITransactionManager transactionManager;

        public ImportBookService(
            IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> exchangeRateRepository,
            IRepository<LedgerPeriod, int> ledgerPeriodRepository,
            IRepository<Supplier, int> supplierRepository,
            IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> orderDetailRepository,
            ITransactionManager transactionManager,
            IPurchaseLedgerPack purchaseLedgerPack)
        {
            this.exchangeRateRepository = exchangeRateRepository;
            this.ledgerPeriodRepository = ledgerPeriodRepository;
            this.supplierRepository = supplierRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.transactionManager = transactionManager;
            this.purchaseLedgerPack = purchaseLedgerPack;
        }

        public IEnumerable<ImportBookExchangeRate> GetExchangeRates(string date)
        {
            var unformattedDate = DateTime.Parse(date);
            var formattedDate = unformattedDate.ToString("MMMYYYY");

            var ledgerPeriod = this.ledgerPeriodRepository.FindBy(x => x.MonthName == formattedDate);

            var exchangeRates = this.exchangeRateRepository.FilterBy(x => x.PeriodNumber == ledgerPeriod.PeriodNumber);

            return exchangeRates;
        }

        public ProcessResult PostDutyForOrderDetails(
            IEnumerable<ImportBookOrderDetail> orderDetails,
            int supplierId,
            int employeeId,
            DateTime postDutyDate)
        {
            foreach (var detail in orderDetails)
            {
                var oldDetail = this.orderDetailRepository.FindById(
                    new ImportBookOrderDetailKey(detail.ImportBookId, detail.LineNumber));

                if (!oldDetail.PostDuty && detail.PostDuty)
                {
                    this.PostDuty(detail, supplierId, employeeId, postDutyDate);

                    oldDetail.PostDuty = true;
                    this.transactionManager.Commit();
                }
            }

            return new ProcessResult(true, "Successfully posted duty");
        }

        public void Update(ImportBook from, ImportBook to)
        {
            this.UpdateTopLevelProperties(from, to);

            this.UpdateInvoiceDetails(from.InvoiceDetails, to.InvoiceDetails);

            this.UpdateOrderDetails(from.OrderDetails, to.OrderDetails);

            this.UpdatePostEntries(from.PostEntries, to.PostEntries);
        }

        private void PostDuty(ImportBookOrderDetail detail, int supplierId, int employeeId, DateTime postDutyDate)
        {
            var accountingCompany = this.supplierRepository.FindById(supplierId).AccountingCompany;

            if (accountingCompany != "LINN")
            {
                throw new PostDutyException(
                    "supplier is not set up for records duty yet, accounting company is not LINN");
            }

            var pltref = this.purchaseLedgerPack.GetNextLedgerSeq();
            var ledgerPeriod = this.purchaseLedgerPack.GetLedgerPeriod();
            var debitNomacc = this.purchaseLedgerPack.GetNomacc("0000002302", "0000012926");

            var newPurchaseLedgerEntry = new PurchaseLedger
                                             {
                                                 Pltref = pltref,
                                                 SupplierId = 7371,
                                                 OrderLine = 1,
                                                 OrderNumber = detail.OrderNumber,
                                                 DatePosted = DateTime.Now,
                                                 PlState = "U",
                                                 PlQuantity = 0,
                                                 PlNetTotal = detail.DutyValue,
                                                 PlVat = 0,
                                                 PlTotal = detail.DutyValue,
                                                 BaseNetTotal = detail.DutyValue,
                                                 BaseVatTotal = 0,
                                                 BaseTotal = detail.DutyValue,
                                                 InvoiceDate = postDutyDate,
                                                 PlInvoiceRef = $"IMP{detail.ImportBookId}",
                                                 PlDeliveryRef = $"DUTY{postDutyDate.ToString("ddMMMyyyy")}",
                                                 CompanyRef = "DUTY",
                                                 Currency = "GBP",
                                                 LedgerPeriod = ledgerPeriod,
                                                 PostedBy = employeeId,
                                                 DebitNomacc = debitNomacc,
                                                 CreditNomacc = 1005,
                                                 PlTransType = "INV",
                                                 BaseCurrency = "GBP",
                                                 Carriage = 0,
                                                 UnderOver = 0,
                                                 ExchangeRate = 1,
                                                 LedgerStream = 1
                                             };
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
                    // below hard coded line number as one may change eventually
                    // if more than one purchase order detail is implemented for a purchase order
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
