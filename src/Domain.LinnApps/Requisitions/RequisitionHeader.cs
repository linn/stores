namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class RequisitionHeader
    {
        public int ReqNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public int? Document1 { get; set; }

        public IEnumerable<RequisitionLine> Lines { get; set; }

        public IEnumerable<ReqMove> Moves { get; set; }

        public decimal? Qty { get; set; }

        public string Document1Name { get; set; }

        public string PartNumber { get; set; }

        public Part Part { get; set; }

        public int? ToLocationId { get; set; }

        public StorageLocation ToLocation { get; set; }

        public string Cancelled { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? DateCancelled { get; set; }

        public string CancelledReason { get; set; }

        public string FunctionCode { get; set; }
    }
}
