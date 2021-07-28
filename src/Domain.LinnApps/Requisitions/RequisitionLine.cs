namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System.Collections.Generic;

    public class RequisitionLine
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public string PartNumber { get; set; }

        public string TransactionCode { get; set; }

        public IEnumerable<ReqMove> Moves { get; set; }
    }
}
