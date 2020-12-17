namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public class WorkstationFacadeService : IWorkstationFacadeService
    {
        public IResult<ResponseModel<WorkstationTopUpStatus>> GetStatus()
        {
            throw new System.NotImplementedException();
        }
    }
}
