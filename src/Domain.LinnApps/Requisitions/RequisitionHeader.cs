namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System;
    using System.Collections.Generic;

    public class RequisitionHeader
    {
        public int ReqNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public int? Document1 { get; set; }

        public IEnumerable<RequisitionLine> Lines { get; set; }

        public IEnumerable<ReqMove> Moves { get; set; }
    }
}
