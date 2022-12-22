namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class RequisitionLine
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public string PartNumber { get; set; }

        public string TransactionCode { get; set; }

        public StoresTransactionDefinition TransactionDefinition { get; set; }

        public IEnumerable<ReqMove> Moves { get; set; }
    }
}
