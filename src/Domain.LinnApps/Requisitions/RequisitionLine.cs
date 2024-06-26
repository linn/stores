﻿namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using System;
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

        public DateTime? DateCancelled { get; set; }
    
        public string CancelledReason { get; set; }

        public int? CancelledBy { get; set; }

        public int? Document1Line { get; set; }

        public IEnumerable<ReqMove> Moves { get; set; }
    }
}
