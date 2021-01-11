namespace Linn.Stores.Domain.LinnApps.Workstation.Models
{
    public class WorkstationTopUpStatus
    {
        public string WorkstationTopUpJobRef { get; set; }

        public string WorkstationTopUpMessage { get; set; }

        public string ProductionTriggerRunJobRef { get; set; }

        public string ProductionTriggerRunMessage { get; set; }

        public string StatusMessage { get; set; }
    }
}
