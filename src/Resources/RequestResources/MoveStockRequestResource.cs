namespace Linn.Stores.Resources.RequestResources
{
    public class MoveStockRequestResource
    {
        public int? ReqNumber { get; set; }

        public string PartNumber { get; set; }

        public int Quantity { get; set; }

        public int? FromPalletNumber { get; set; }

        public int? FromLocationId { get; set; }

        public string From { get; set; }

        public string FromStockRotationDate { get; set; }

        public string FromStockPoolCode { get; set; }

        public string FromState { get; set; }

        public int? ToPalletNumber { get; set; }

        public int? ToLocationId { get; set; }

        public string To { get; set; }

        public string ToStockRotationDate { get; set; }

        public int UserNumber { get; set; }

        public string StorageType { get; set; }
    }
}
