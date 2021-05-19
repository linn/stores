namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
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
            ImportBookInvoiceDetail invoiceDetail,
            ImportBookOrderDetail orderDetail,
            ImportBookPostEntry postEntry)
        {

        }
    }
}
