namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StoresMoveLog
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public DateTime? DateProcessed { get; set; }

        public int ReqNumber { get; set; }

        public int ReqLine { get; set; }

        public int BudgetId { get; set; }

        public string TransactionCode { get; set; }

        public string CreatedBy { get; set; }

        public decimal Qty { get; set; }

        public string FromLocation { get; set; }

        public string FromBatchRef { get; set; }

        public DateTime FromBatchDate { get; set; }

        public string FromState { get; set; }

        public string FromStockPool { get; set; }

        public decimal? FromQtyBefore { get; set; }

        public decimal? FromQtyAfter { get; set; }

        public string ToLocation { get; set; }

        public string ToBatchRef { get; set; }

        public DateTime ToBatchDate { get; set; }

        public string ToState { get; set; }

        public string ToStockPool { get; set; }

        public decimal? ToQtyBefore { get; set; }

        public decimal? ToQtyAfter { get; set; }

        public decimal? QtyInStock { get; set; }

        public decimal? QtyInQC { get; set; }

        public decimal? QtyAtSupplier { get; set;  }
    }
}
