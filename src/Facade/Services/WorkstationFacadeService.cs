namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public class WorkstationFacadeService : IWorkstationFacadeService
    {
        private readonly IWorkstationService workstationService;

        public WorkstationFacadeService(IWorkstationService workstationService)
        {
            this.workstationService = workstationService;
        }

        public IResult<ResponseModel<WorkstationTopUpStatus>> GetStatus(IEnumerable<string> privileges)
        {
            return new SuccessResult<ResponseModel<WorkstationTopUpStatus>>(
                new ResponseModel<WorkstationTopUpStatus>(
                    this.workstationService.GetTopUpStatus(),
                    privileges));
        }

        public IResult<ResponseModel<WorkstationTopUpStatus>> StartTopUpRun(IEnumerable<string> privileges)
        {
            throw new System.NotImplementedException();
        }
    }
}
