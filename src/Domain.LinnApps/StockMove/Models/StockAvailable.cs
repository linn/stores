namespace Linn.Stores.Domain.LinnApps.StockMove.Models
{
    using System;

    public class StockAvailable
    {
        public string PartNumber { get; set; }

        public int QuantityAvailable { get; set; }

        public DateTime StockRotationDate { get; set; }

        public int? LocationId { get; set; }

        public string LocationCode { get; set; }

        public int? PalletNumber { get; set; }

        public string StockPoolCode { get; set; }

        public string State { get; set; }
    }
}
