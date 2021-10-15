namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class ImportBookService : IImportBookService
    {
        private readonly IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> exchangeRateRepository;
        private readonly IRepository<LedgerPeriod, int> ledgerPeriodRepository;
        private readonly IRepository<Supplier, int> supplierRepository;
        private readonly IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> orderDetailRepository;
        private readonly ITransactionManager transactionManager;

        public ImportBookService(
            IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> exchangeRateRepository,
            IRepository<LedgerPeriod, int> ledgerPeriodRepository,
            IRepository<Supplier, int> supplierRepository,
        IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> orderDetailRepository,
            ITransactionManager transactionManager)
        {
            this.exchangeRateRepository = exchangeRateRepository;
            this.ledgerPeriodRepository = ledgerPeriodRepository;
            this.supplierRepository = supplierRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.transactionManager = transactionManager;
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

        public void PostDutyForOrderDetails(IEnumerable<ImportBookOrderDetail> orderDetails, int supplierId)
        {
            foreach (var detail in orderDetails)
            {
                var dbDetail = this.orderDetailRepository
                    .FindById(new ImportBookOrderDetailKey(detail.ImportBookId, detail.LineNumber));

                if (!dbDetail.PostDuty && detail.PostDuty)
                {
                    try
                    {
                        PostDuty(detail, supplierId);
                    }
                    catch (Exception e)
                    {

                    }

                    dbDetail.PostDuty = true;
                    this.transactionManager.Commit();
                }

              
            }

            this.tr
        }

        private void PostDuty(ImportBookOrderDetail detail, int supplierId)
        {
            var accountingCompany = supplierRepository.FindById(supplierId).AccountingCompany;
            var supplierIdToInsert = 7371;
            var departmentToInsert = "0000002302";

            if (accountingCompany != "LINN")
            {
                throw new PostDutyException("supplier is not set up for records duty yet, accounting company is not LINN");
            }

           

            //add post duty flag to order details

            //		INSERT INTO PURCHASE_LEDGER(PL_TREF,
            //														   SUPPLIER_ID,
            //														   ORDER_LINE,
            //														   ORDER_NUMBER,
            //														   DATE_POSTED,
            //														   PL_STATE,
            //														   PL_QTY,
            //														   PL_NET_TOTAL,
            //														   PL_VAT,
            //														   PL_TOTAL,
            //														   BASE_NET_TOTAL,
            //														   BASE_VAT_TOTAL,
            //														   BASE_TOTAL,
            //														   INVOICE_DATE,
            //														   PL_INVOICE_REF,
            //														   PL_DELIVERY_REF,
            //														   COMPANY_REF,
            //														   CURRENCY,
            //														   LEDGER_PERIOD,
            //														   POSTED_BY,
            //														   DEBIT_NOMACC,
            //														   CREDIT_NOMACC,
            //														   PL_TRANS_TYPE,
            //														   BASE_CURRENCY,
            //														   CARRIAGE,
            //														   UNDER_OVER,
            //														   EXCHANGE_RATE,
            //														   LEDGER_STREAM)
            //		VALUES(cg_code_controls_next_val('PL_LEDGER_SEQ', 1), --PL_TREF,
            //																v_supplier, --SUPPLIER_ID,
            //																1, --ORDER_LINE,
            //																:IMPBOOK_ORDER_DETAIL.ORDER_NUMBER, --ORDER_NUMBER
            //																SYSDATE, --DATE_POSTED,
            //																'U', --PL_STATE,
            //																0, --PL_QTY,
            //																:IMPBOOK_ORDER_DETAIL.DUTY_VALUE, --PL_NET_TOTAL
            //																0, --PL_VAT
            //																:IMPBOOK_ORDER_DETAIL.DUTY_VALUE, --PL_TOTAL
            //																:IMPBOOK_ORDER_DETAIL.DUTY_VALUE, --BASE_NET_TOTAL
            //																0, --BASE_VAT_TOTAL
            //																:IMPBOOK_ORDER_DETAIL.DUTY_VALUE, --BASE_TOTAL
            //																:IMPBOOK_ORDER_DETAIL.POST_INVOICE_DATE, --INVOICE_DATE,
            //																'IMP' ||    :IMPBOOK.IMPBOOK_ID, --PL_INVOICE_REF
            //																'DUTY' || to_char(:IMPBOOK_ORDER_DETAIL.POST_INVOICE_DATE, 'DDMONYYYY'), --PL_DELIVERY_REF,
            //																'DUTY', --COMPANY_REF
            //																'GBP', --CURRENCY
            //																pl_pack.get_pl_ledger_period, --LEDGER_PERIOD
            //																USER_PACK.CURRENT_USER_NUMBER, --POSTED_BY,
            //																pl_pack.get_nomacc(v_department, '0000012926'), --1006, --DEBIT_NOMACC
            //																1005, --CREDIT_NOMACC,
            //																'INV', --PL_TRANS_TYPE
            //																'GBP', --BASE_CURRENCY
            //																0, --CARRIAGE
            //																0, --UNDER_OVER
            //																1, --EXCHANGE_RATE,
            //																1-- LEDGER_STREAM
            //																);

            return;
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
