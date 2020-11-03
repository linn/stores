﻿namespace Linn.Stores.Resources.Allocation
{
    using Linn.Common.Resources;

    public class SosAllocHeadResource : HypermediaResource
    {
        public int JobId { get; set; }

        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public string EarliestRequestedDate { get; set; }

        public int OldestOrder { get; set; }

        public decimal ValueToAllocate { get; set; }

        public string OutletHoldStatus { get; set; }
    }
}