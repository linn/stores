namespace Linn.Stores.Resources.Requisitions
{
    using System;

    public class RequisitionLineResource
    {
        public string TransactionCode { get; set; }

        public int Line { get; set; }

        public string DateCancelled { get; set; }

        public string CancelledReason { get; set; }

        public int? CancelledBy { get; set; }

        public int? Document1Line { get; set; }
    }
}
