namespace Linn.Stores.Resources.GoodsIn
{
    public class BookInResultResource : ProcessResultResource
    {
        public int? ReqNumber { get; set; }

        public string QcState { get; set; }

        public string DocType { get; set; }

        public string QcInfo { get; set; }

        public string TransactionCode { get; set; }
    }
}
