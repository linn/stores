namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class ReqMove
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public int Sequence { get; set; }

        public decimal Quantity { get; set; }

        public int? StockLocatorId { get; set; }

        public StockLocator StockLocator { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public StorageLocation Location { get; set; }

        public string StockPoolCode { get; set; }

        public string Booked { get; set; }

        public string Remarks { get; set; }

        public RequisitionHeader Header { get; set; }

        public DateTime? DateBooked { get; set; }

        public DateTime? DateCancelled { get; set; }
    }
}
