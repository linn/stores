namespace Linn.Stores.Resources.GoodsIn
{
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
    }
}
