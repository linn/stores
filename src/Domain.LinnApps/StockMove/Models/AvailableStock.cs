namespace Linn.Stores.Domain.LinnApps.StockMove.Models
{
    using System;

    public class AvailableStock
    {
        public string PartNumber { get; set; }

        public int QuantityAvailable { get; set; }

        public DateTime StockRotationDate { get; set; }

        public int? LocationId { get; set; }

        public string LocationCode { get; set; }

        public int? PalletNumber { get; set; }

        public string StockPoolCode { get; set; }

        public string State { get; set; }

        public string DisplayLocation { get; set; }

        public string DisplayMoveLocation { get; set; }
    }
}
