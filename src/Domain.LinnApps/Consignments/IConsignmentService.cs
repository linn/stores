namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System.Collections.Generic;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IConsignmentService
    {
        void CloseConsignment(Consignment consignment, int closedById);

        ProcessResult PrintConsignmentDocuments(int consignmentId, int userNumber);

        PackingList GetPackingList(int consignmentId);

        ProcessResult SaveConsignmentDocuments(int resourceConsignmentId);

        IEnumerable<Consignment> GetByInvoiceNumber(int invoiceNumber);
    }
}
