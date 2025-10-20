namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IConsignmentService
    {
        Task CloseConsignment(Consignment consignment, int closedById);

        Task<ProcessResult> PrintConsignmentDocuments(int consignmentId, int userNumber);

        PackingList GetPackingList(int consignmentId);

        ProcessResult SaveConsignmentDocuments(int resourceConsignmentId);

        IEnumerable<Consignment> GetByInvoiceNumber(int invoiceNumber);
    }
}
