namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using Linn.Stores.Domain.LinnApps.Models;

    public class BookinResult : ProcessResult
    {
        public BookinResult(bool success, string message) : base(success, message)
        {
        }

        public int? ReqNumber { get; set; }

        public string QcState { get; set; }

        public string DocType { get; set; }

        public string QcInfo { get; set; }

        public string TransactionCode { get; set; }
    }
}
