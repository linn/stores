﻿namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    public class PackingListItem
    {
        public PackingListItem(int? pallet, int? box, string contentsDescription, decimal quantity)
        {
            this.Pallet = pallet;
            this.Box = box;
            this.ContentsDescription = contentsDescription;
            this.Quantity = quantity;
        }

        public int? Pallet { get; set; }

        public int? Box { get; set; }

        public int? To { get; set; }

        public int? Count { get; set; }

        public decimal Quantity { get; set; }

        public string ContentsDescription { get; set; }
    }
}
