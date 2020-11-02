namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;

    public class SosAllocHead
    {
        public int JobId { get; set; }

        public int  AccountId { get; set; }

        public int OutletNumber { get; set; }

        public DateTime EarliestRequestedDate { get; set; }

        public int OldestOrder { get; set; }

        public decimal ValueToAllocate { get; set; }

        public string OutletHoldStatus { get; set; }
    }
}
