namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using Linn.Stores.Domain.LinnApps.Models;

    public class BookinResult : ProcessResult
    {
        public BookinResult(bool success, string message) : base(success, message)
        {
        }

        public int? ReqNumber { get; set; }

    }
}
