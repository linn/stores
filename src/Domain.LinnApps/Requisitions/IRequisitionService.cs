namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public interface IRequisitionService
    {
        RequisitionActionResult Unallocate(int reqNumber, int? reqLine, int userNumber);
    }
}
