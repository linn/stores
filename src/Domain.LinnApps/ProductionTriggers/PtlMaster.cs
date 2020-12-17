namespace Linn.Stores.Domain.LinnApps.ProductionTriggers
{
    using System;

    public class PtlMaster
    {
        public string LastFullJobRef { get; set; }

        public DateTime LastFullRunDate { get; set; }

        public int LastFullRunMinutesTaken { get; set; }

        public int LastDaysToLookAhead { get; set; }

        public string Status { get; set; }
    }
}
