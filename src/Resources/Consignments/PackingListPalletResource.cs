namespace Linn.Stores.Resources.Consignments
{
    using System.Collections.Generic;

    public class PackingListPalletResource
    {
        public int PalletNumber { get; set; }

        public IEnumerable<PackingListItemResource> Items { get; set; }

        public string DisplayWeight { get; set; }

        public string DisplayDimensions { get; set; }

        public decimal? Volume { get; set; }
    }
}
