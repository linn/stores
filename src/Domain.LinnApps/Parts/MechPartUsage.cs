namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public class MechPartUsage
    {
        public int SourceId { get; set; }

        public MechPartSource Source { get; set; }

        public int QuantityUsed { get; set; }

        public string RootProductName { get; set; }

        public RootProduct RootProduct { get; set; }
    }
}
