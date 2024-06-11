namespace Linn.Stores.Resources.Requisitions
{
    using System;

    public class RequisitionLineResource
    {
        public string TransactionCode { get; set; }

        public int Line { get; set; }

        public DateTime? DateCancelled { get; set; }

        public string CancelledReason { get; set; }

        public int? CancelledBy { get; set; }
    }
}
