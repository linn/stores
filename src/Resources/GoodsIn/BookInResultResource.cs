namespace Linn.Stores.Resources.GoodsIn
{
    using System.Collections.Generic;

    public class BookInResultResource : ProcessResultResource
    {
        public int? ReqNumber { get; set; }

        public string QcState { get; set; }

        public string DocType { get; set; }

        public string QcInfo { get; set; }

        public string TransactionCode { get; set; }

        public int QtyReceived { get; set; }

        public string UnitOfMeasure { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string KardexLocation { get; set; }

        public bool CreateParcel { get; set; }

        public string ParcelComments { get; set; }

        public int? SupplierId { get; set; }

        public int? CreatedBy { get; set; }

        public bool PrintLabels { get; set; }

        public IEnumerable<GoodsInLogEntryResource> Lines { get; set; }
    }
}
