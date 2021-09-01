namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using Linn.Stores.Domain.LinnApps.Models;

    public class BookInResult : ProcessResult
    {
        public BookInResult(bool success, string message) : base(success, message)
        {
        }

        public int? ReqNumber { get; set; }

        public string QcState { get; set; }

        public string DocType { get; set; }

        public string QcInfo { get; set; }

        public string TransactionCode { get; set; }

        public string UnitOfMeasure { get; set; }

        public int QtyReceived { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string KardexLocation { get; set; }

        public bool CreateParcel { get; set; }

        public string ParcelComments { get; set; }

        public int? SupplierId { get; set; }

        public int? CreatedBy { get; set; }
    }
}
