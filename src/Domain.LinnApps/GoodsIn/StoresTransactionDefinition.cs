namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class StoresTransactionDefinition
    {
        public string TransactionCode { get; set; }

        public string Description { get; set; }

        public string QcType { get; set; }

        public string DocType { get; set; }

        public IEnumerable<RequisitionLine> RequisitionLines { get; set; }
    }
}
