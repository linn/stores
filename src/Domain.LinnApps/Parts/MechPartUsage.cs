﻿namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class MechPartUsage
    {
        public int SourceId { get; set; }

        public MechPartSource Source { get; set; }

        public int QuantityUsed { get; set; }

        public string Product { get; set; }
    }
}
