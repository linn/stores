namespace Linn.Stores.Resources.Allocation
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class DespatchPalletQueueResultResource : HypermediaResource
    {
        public int TotalNumberOfPallets { get; set; }

        public int NumberOfPalletsToMove { get; set; }

        public IEnumerable<DespatchPalletQueueDetailResource> DespatchPalletQueueDetails { get; set; }
    }
}
