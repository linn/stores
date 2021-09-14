namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IConsignmentService
    {
        void CloseConsignment(Consignment consignment, int closedById);

        ProcessResult PrintConsignmentDocuments(int consignmentId, int userNumber);
    }
}
