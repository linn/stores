namespace Linn.Stores.Domain.LinnApps.Workstation
{
    using System;

    public class TopUpListJobRef
    {
        public string JobRef { get; set; }

        public DateTime DateRun { get; set; }

        public string FullRun { get; set; }
    }
}
