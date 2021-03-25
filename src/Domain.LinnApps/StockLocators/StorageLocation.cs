namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class StorageLocation
    {
        public int LocationId { get; set; }

        public string LocationCode { get; set; }

        public string Description { get; set; }

        public string LocationType { get; set; }

        public DateTime? DateInvalid { get; set; }

        public IEnumerable<StockLocator> StockLocators { get; set; }

        public IEnumerable<StockLocatorLocation> StockLocatorLocations { get; set; }

        public IEnumerable<ReqMove> ReqMoves { get; set; }
    }
}
