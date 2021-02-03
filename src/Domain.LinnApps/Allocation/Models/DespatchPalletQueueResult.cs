namespace Linn.Stores.Domain.LinnApps.Allocation.Models
{
    using System.Collections.Generic;

    public class DespatchPalletQueueResult
    {
        public int TotalNumberOfPallets { get; set; }

        public int NumberOfPalletsToMove { get; set; }

        public IEnumerable<DespatchPalletQueueDetail> DespatchPalletQueueDetails { get; set; }
    }
}
