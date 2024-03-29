﻿namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Models;

    public class BookInResult : ProcessResult
    {
        public BookInResult(bool success, string message) : base(success, message)
        {
        }

        public int? OrderNumber { get; set; }

        public int UserNumber { get; set; }

        public int? ReqNumber { get; set; }

        public string QcState { get; set; }

        public string DocType { get; set; }

        public string QcInfo { get; set; }

        public string TransactionCode { get; set; }

        public string UnitOfMeasure { get; set; }

        public decimal QtyReceived { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string KardexLocation { get; set; }

        public bool CreateParcel { get; set; }

        public string ParcelComments { get; set; }

        public int? SupplierId { get; set; }

        public int? CreatedBy { get; set; }

        public IEnumerable<GoodsInLogEntry> Lines { get; set; }

        public bool PrintLabels { get; set; } = false;
    }
}
