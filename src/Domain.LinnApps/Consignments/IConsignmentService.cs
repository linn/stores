namespace Linn.Stores.Domain.LinnApps.Consignments
{
    public interface IConsignmentService
    {
        void CloseConsignment(Consignment consignment, int closedById);
    }
}
