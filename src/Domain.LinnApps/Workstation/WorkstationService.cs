namespace Linn.Stores.Domain.LinnApps.Workstation
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public class WorkstationService : IWorkstationService
    {
        private readonly ISingleRecordRepository<PtlMaster> ptlMasterRepository;

        private readonly IRepository<TopUpListJobRef, string> topUpListJobRefRepository;

        private readonly IWorkstationPack workstationPack;

        public WorkstationService(
            ISingleRecordRepository<PtlMaster> ptlMasterRepository,
            IRepository<TopUpListJobRef, string> topUpListJobRefRepository,
            IWorkstationPack workstationPack)
        {
            this.ptlMasterRepository = ptlMasterRepository;
            this.topUpListJobRefRepository = topUpListJobRefRepository;
            this.workstationPack = workstationPack;
        }

        public static bool CanStartNewRun(WorkstationTopUpStatus status, string progressStatus)
        {
            if (status.ProductionTriggerRunJobRef == status.WorkstationTopUpJobRef)
            {
                return false;
            }

            return string.IsNullOrEmpty(progressStatus);
        }

        public WorkstationTopUpStatus GetTopUpStatus()
        {
            var triggerRunStatus = this.ptlMasterRepository.GetRecord();

            var topUpRun = this.topUpListJobRefRepository.FindAll().ToList().OrderByDescending(a => a.JobRef).FirstOrDefault();

            return new WorkstationTopUpStatus
                       {
                           ProductionTriggerRunJobRef = triggerRunStatus.LastFullJobRef,
                           ProductionTriggerRunMessage = $"The last run was on {triggerRunStatus.LastFullRunDate:dd-MMM-yyyy} at {triggerRunStatus.LastFullRunDate:h:mm tt} and took {triggerRunStatus.LastFullRunMinutesTaken} minutes.",
                           WorkstationTopUpJobRef = topUpRun != null ? topUpRun.JobRef : "No run today",
                           WorkstationTopUpMessage = topUpRun != null ? $"The last run was on {topUpRun.DateRun:dd-MMM-yyyy} at {topUpRun.DateRun:h:mm tt}." : string.Empty,
                           StatusMessage = this.workstationPack.TopUpRunProgressStatus()
                       };
        }

        public WorkstationTopUpStatus StartTopUpRun()
        {
            var status = this.GetTopUpStatus();

            if (!CanStartNewRun(status, this.workstationPack.TopUpRunProgressStatus()))
            {
                status.StatusMessage = "Workstation top run is in progress or already completed";
                return status;
            }

            this.workstationPack.StartTopUpRun();

            status.StatusMessage = "Workstation top up run has been started";
            return status;
        }

        public bool CanStartNewRun()
        {
            return CanStartNewRun(this.GetTopUpStatus(), this.workstationPack.TopUpRunProgressStatus());
        }
    }
}
