namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public interface IRequisitionActionsFacadeService
    {
        IResult<RequisitionActionResult> Unallocate(int reqNumber, int? lineNumber, int? userNumber);

        IResult<IEnumerable<ReqMove>> GetMoves(int reqNumber);
    }
}
