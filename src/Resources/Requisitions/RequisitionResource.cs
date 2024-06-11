namespace Linn.Stores.Resources.Requisitions
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class RequisitionResource : HypermediaResource
    {
        public int ReqNumber { get; set; }

        public int? Document1 { get; set; }

        public string DocumentType { get; set; }

        public string QcState { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public decimal? QtyReceived { get; set; }

        public string UnitOfMeasure { get; set; }

        public string QcInfo { get; set; }

        public string StorageType { get; set; }

        public string Cancelled { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? DateCancelled { get; set; }

        public string CancelledReason { get; set; }

        public string FunctionCode { get; set; }

        public IEnumerable<RequisitionLineResource> Lines { get; set; }
    }
}
