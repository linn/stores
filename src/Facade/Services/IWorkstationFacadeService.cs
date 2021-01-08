namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public interface IWorkstationFacadeService
    {
        IResult<ResponseModel<WorkstationTopUpStatus>> GetStatus(IEnumerable<string> privileges);
    }
}
