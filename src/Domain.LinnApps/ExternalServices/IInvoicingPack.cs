namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IInvoicingPack
    {
        ProcessResult InvoiceConsignment(int consignmentId, int closedById);
    }
}
