namespace Linn.Stores.Domain.LinnApps.Workstation
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public class WorkstationService : IWorkstationService
    {
        private readonly ISingleRecordRepository<PtlMaster> ptlMasterRepository;

        private readonly IRepository<TopUpListJobRef, string> topUpListJobRefRepository;

        public WorkstationService(
            ISingleRecordRepository<PtlMaster> ptlMasterRepository,
            IRepository<TopUpListJobRef, string> topUpListJobRefRepository)
        {
            this.ptlMasterRepository = ptlMasterRepository;
            this.topUpListJobRefRepository = topUpListJobRefRepository;
        }

        public WorkstationTopUpStatus GetTopUpStatus()
        {
            var triggerRunStatus = this.ptlMasterRepository.GetRecord();

            var topUpRun = this.topUpListJobRefRepository.FindAll().ToList().OrderByDescending(a => a.JobRef).FirstOrDefault();

            return new WorkstationTopUpStatus
                       {
                           ProductionTriggerRunJobRef = triggerRunStatus.LastFullJobRef,
                           ProductionTriggerRunMessage = $"'The last run was on {triggerRunStatus.LastFullRunDate.ToShortDateString()} at {triggerRunStatus.LastFullRunDate.ToShortTimeString()} and took {triggerRunStatus.LastFullRunMinutesTaken} minutes.'",
                           WorkstationTopUpJobRef = topUpRun != null ? topUpRun.JobRef : "No run today",
                           WorkstationTopUpMessage = topUpRun != null ? $"The last run was on {topUpRun.DateRun.ToShortDateString()} at {topUpRun.DateRun.ToShortTimeString()}" : string.Empty
                       };
        }
    }
}
