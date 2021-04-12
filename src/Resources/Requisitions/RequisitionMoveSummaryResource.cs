namespace Linn.Stores.Resources.Requisitions
{
    public class RequisitionMoveSummaryResource
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public int MoveSeq { get; set; }

        public string PartNumber { get; set; }

        public int MoveQuantity { get; set; }

        public int? FromPalletNumber { get; set; }

        public string FromLocationCode { get; set; }

        public int? ToPalletNumber { get; set; }

        public string ToLocationCode { get; set; }
    }
}
