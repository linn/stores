namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public interface IRequisitionActionsFacadeService
    {
        IResult<RequisitionActionResult> Unallocate(int reqNumber, int? lineNumber, int? userNumber);
    }
}
