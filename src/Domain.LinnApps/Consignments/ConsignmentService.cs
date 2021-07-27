namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExportBooks;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class ConsignmentService : IConsignmentService
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IConsignmentProxyService consignmentProxyService;

        private readonly IInvoicingPack invoicingPack;

        private readonly IExportBookPack exportBookPack;

        private readonly IPrintInvoiceDispatcher printInvoiceDispatcher;

        private readonly IPrintConsignmentNoteDispatcher printConsignmentNoteDispatcher;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IRepository<ExportBook, int> exportBookRepository;

        public ConsignmentService(
            IRepository<Employee, int> employeeRepository,
            IRepository<Consignment, int> consignmentRepository,
            IRepository<ExportBook, int> exportBookRepository,
            IConsignmentProxyService consignmentProxyService,
            IInvoicingPack invoicingPack,
            IExportBookPack exportBookPack,
            IPrintInvoiceDispatcher printInvoiceDispatcher,
            IPrintConsignmentNoteDispatcher printConsignmentNoteDispatcher)
        {
            this.employeeRepository = employeeRepository;
            this.consignmentProxyService = consignmentProxyService;
            this.invoicingPack = invoicingPack;
            this.exportBookPack = exportBookPack;
            this.printInvoiceDispatcher = printInvoiceDispatcher;
            this.printConsignmentNoteDispatcher = printConsignmentNoteDispatcher;
            this.consignmentRepository = consignmentRepository;
            this.exportBookRepository = exportBookRepository;
        }

        public void CloseConsignment(Consignment consignment, int closedById)
        {
            if (closedById <= 0)
            {
                throw new ConsignmentCloseException(
                    $"Could not close consignment {consignment.ConsignmentId}. A valid closed by id must be supplied");
            }

            if (consignment.Status != "L")
            {
                throw new ConsignmentCloseException(
                    $"Could not close consignment {consignment.ConsignmentId} is already closed");
            }

            var canClose = this.consignmentProxyService.CanCloseAllocation(consignment.ConsignmentId);

            if (!canClose.Success)
            {
                throw new ConsignmentCloseException(
                    $"Cannot close consignment {consignment.ConsignmentId}. {canClose.Message}");
            }

            var invoiceResult = this.invoicingPack.InvoiceConsignment(consignment.ConsignmentId, closedById);

            if (!invoiceResult.Success)
            {
                throw new ConsignmentCloseException(
                    $"Failed to invoice consignment {consignment.ConsignmentId}. {invoiceResult.Message}");
            }

            if (consignment.HubId.HasValue)
            {
                this.exportBookPack.MakeExportBookFromConsignment(consignment.ConsignmentId);
            }

            this.PrintDocuments(consignment);
        }

        private void PrintDocuments(Consignment consignment)
        {
            var updatedConsignment = this.consignmentRepository.FindById(consignment.ConsignmentId);
            var numberOfCopies = consignment.Address.Country.NumberOfCopiesOfDispatchDocuments ?? 1;

            if (consignment.Address.Country.CountryCode != "GB")
            {
                this.PrintConsignmentNote(consignment, numberOfCopies);
            }

            this.PrintInvoices(updatedConsignment, numberOfCopies);

            this.MaybePrintExportBook(consignment);
        }

        private void PrintInvoices(Consignment updatedConsignment, int numberOfCopies)
        {
            foreach (var consignmentInvoice in updatedConsignment.Invoices)
            {
                for (var i = 1; i <= numberOfCopies; i++)
                {
                    this.printInvoiceDispatcher.PrintInvoice(
                        consignmentInvoice.DocumentNumber,
                        consignmentInvoice.DocumentType,
                        "CUSTOMER MASTER",
                        "Y");
                    this.printInvoiceDispatcher.PrintInvoice(
                        consignmentInvoice.DocumentNumber,
                        consignmentInvoice.DocumentType,
                        "DELIVERY NOTE",
                        "N");
                }
            }
        }

        private void PrintConsignmentNote(Consignment consignment, int numberOfCopies)
        {
            for (var i = 1; i <= numberOfCopies; i++)
            {
                this.printConsignmentNoteDispatcher.PrintConsignmentNote(consignment.ConsignmentId);
            }
        }

        private void MaybePrintExportBook(Consignment consignment)
        {
            var exportBooks =
                this.exportBookRepository.FilterBy(a => a.ConsignmentId == consignment.ConsignmentId);
            foreach (var exportBook in exportBooks)
            {
                this.printInvoiceDispatcher.PrintInvoice(exportBook.ExportId, "E", "CUSTOMER MASTER", "Y");
            }
        }
    }
}
