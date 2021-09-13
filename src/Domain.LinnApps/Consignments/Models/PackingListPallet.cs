namespace Linn.Stores.Domain.LinnApps.Consignments.Models
{
    using System.Collections.Generic;

    public class PackingListPallet
    {
        public int PalletNumber { get; set; }

        public IList<PackingListItem> Items { get; set; }

        public string DisplayWeight { get; set; }

        public string DisplayDimensions { get; set; }

        public decimal? Volume { get; set; }

        public decimal? Weight { get; set; }
    }
}
