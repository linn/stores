namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RequisitionLine
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public string PartNumber { get; set; }

        public string TransactionCode { get; set; }

        public StoresTransactionDefinition TransactionDefinition { get; set; }

        public IEnumerable<ReqMove> Moves { get; set; }

        public string TransactionCode { get; set; }
    }
}
