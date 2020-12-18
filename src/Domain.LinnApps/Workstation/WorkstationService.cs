namespace Linn.Stores.Domain.LinnApps.Workstation
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public class WorkstationService : IWorkstationService
    {
        private readonly ISingleRecordRepository<PtlMaster> ptlMasterRepository;

        public WorkstationService(ISingleRecordRepository<PtlMaster> ptlMasterRepository)
        {
            this.ptlMasterRepository = ptlMasterRepository;
        }

        public WorkstationTopUpStatus GetTopUpStatus()
        {
            var triggerRunStatus = this.ptlMasterRepository.GetRecord();

            return new WorkstationTopUpStatus
                       {
                           ProductionTriggerRunJobRef = triggerRunStatus.LastFullJobRef
                       };
        }
    }
}
