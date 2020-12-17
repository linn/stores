namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public interface IWorkstationFacadeService
    {
        IResult<ResponseModel<WorkstationTopUpStatus>> GetStatus();
    }
}
