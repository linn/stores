namespace Linn.Stores.Resources.Tpk
{
    public class TransferableStockResource
    {
        public int? LocationId { get; set; }

        public string LocationCode { get; set; }

        public int? PalletNumber { get; set; }

        public string FromLocation { get; set; }

        public string StoragrPlaceDescription { get; set; }

        public int VaxPallet { get; set; }

        public string ArticleNumber { get; set; }

        public int? Quantity { get; set; }

        public string InvoiceDescription { get; set; }

        public int? ConsignmentId { get; set; }

        public string Addressee { get; set; }

        public int? ReqNumber { get; set; }

        public int? ReqLine { get; set; }

        public string DespatchLocationCode { get; set; }

        public int? OrderNumber { get; set; }

        public int? OrderLine { get; set; }
    }
}
