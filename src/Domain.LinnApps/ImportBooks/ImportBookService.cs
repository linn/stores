namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System.Collections.Generic;
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
            IEnumerable<ImportBookInvoiceDetail> invoiceDetails,
            IEnumerable<ImportBookOrderDetail> orderDetails,
            IEnumerable<ImportBookPostEntry> postEntries)
        {
            this.UpdateInvoiceDetails(invoiceDetails);

            this.UpdateOrderDetails(orderDetails);

            this.UpdatePostEntries(postEntries);
        }

        public void UpdateInvoiceDetails(IEnumerable<ImportBookInvoiceDetail> details)
        {
            foreach (var detail in details)
            {
                var currentDetail = this.InvoiceDetailRepository.FindById(new ImportBookInvoiceDetailKey(detail.ImportBookId, detail.LineNumber));

                if (currentDetail == null)
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

        public void UpdateOrderDetails(IEnumerable<ImportBookOrderDetail> details)
        {
            foreach (var detail in details)
            {
                var currentDetail = this.OrderDetailRepository.FindById(new ImportBookOrderDetailKey(detail.ImportBookId, detail.LineNumber));

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

        public void UpdatePostEntries(IEnumerable<ImportBookPostEntry> entries)
        {
            foreach (var entry in entries)
            {
                var currentEntry = this.PostEntryRepository.FindById(new ImportBookPostEntryKey(entry.ImportBookId, entry.LineNumber));

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

        public void CreateInvoiceDetail(ImportBookInvoiceDetail detail)
        {
           this.InvoiceDetailRepository.Add(detail);
        }

        public void CreateOrderDetail(ImportBookOrderDetail detail)
        {
            this.OrderDetailRepository.Add(detail);
        }
        
        public void CreatePostEntry(ImportBookPostEntry entry)
        {
            this.PostEntryRepository.Add(entry);
        }
    }
}
