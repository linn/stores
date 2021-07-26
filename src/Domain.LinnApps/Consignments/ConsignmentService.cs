namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class ConsignmentService : IConsignmentService
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IConsignmentProxyService consignmentProxyService;

        private readonly IInvoicingPack invoicingPack;

        public ConsignmentService(
            IRepository<Employee, int> employeeRepository,
            IConsignmentProxyService consignmentProxyService,
            IInvoicingPack invoicingPack)
        {
            this.employeeRepository = employeeRepository;
            this.consignmentProxyService = consignmentProxyService;
            this.invoicingPack = invoicingPack;
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
        }
    }
}
