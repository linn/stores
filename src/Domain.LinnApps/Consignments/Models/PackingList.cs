namespace Linn.Stores.Domain.LinnApps.Consignments.Models
{
    using System;
    using System.Collections.Generic;

    public class PackingList
    {
        public int ConsignmentId { get; set; }

        public Address DeliveryAddress { get; set; }

        public Address SenderAddress { get; set; }

        public DateTime? DespatchDate { get; set; }

        public IList<PackingListItem> Items { get; set; }

        public IList<PackingListPallet> Pallets { get; set; }

        public int NumberOfPallets { get; set; }

        public int NumberOfItemsNotOnPallets { get; set; }

        public decimal TotalGrossWeight { get; set; }

        public decimal TotalVolume { get; set; }
    }
}
