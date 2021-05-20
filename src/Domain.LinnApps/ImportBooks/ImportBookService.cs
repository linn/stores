namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System.Collections.Generic;
    using System.ComponentModel;

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
                if (this.InvoiceDetailRepository.FindById(new ImportBookInvoiceDetailKey(detail.ImportBookId, detail.LineNumber)) == null)
                {
                    this.CreateInvoiceDetail(detail);
                }
                else
                {
                    this.UpdateInvoiceDetail(detail);
                }
            }
        }

        public void UpdateOrderDetails(IEnumerable<ImportBookOrderDetail> details)
        {
            foreach (var detail in details)
            {
                if (this.OrderDetailRepository.FindById(new ImportBookOrderDetailKey(detail.ImportBookId, detail.LineNumber)) == null)
                {
                    this.CreateOrderDetail(detail);
                }
                else
                {
                    this.UpdateOrderDetail(detail);
                }
            }
        }

        public void UpdatePostEntries(IEnumerable<ImportBookPostEntry> entries)
        {
            foreach (var entry in entries)
            {
                if (this.PostEntryRepository.FindById(new ImportBookPostEntryKey(entry.ImportBookId, entry.LineNumber)) == null)
                {
                    this.CreatePostEntry(entry);
                }
                else
                {
                    this.UpdatePostEntry(entry);
                }
            }
        }

        public void CreateInvoiceDetail(ImportBookInvoiceDetail detail)
        {
            //create
        }

        public void UpdateInvoiceDetail(ImportBookInvoiceDetail detail)
        {
            //update
        }

        public void CreateOrderDetail(ImportBookOrderDetail detail)
        {
            //create
        }

        public void UpdateOrderDetail(ImportBookOrderDetail detail)
        {
            //update

        }

        public void CreatePostEntry(ImportBookPostEntry detail)
        {
            //create
        }

        public void UpdatePostEntry(ImportBookPostEntry detail)
        {
            //update

        }
    }
}
