namespace Linn.Stores.Domain.LinnApps.Allocation
{
    public class DespatchPickingSummary
    {
        public string FromPlace { get; set; }

        public string Addressee { get; set; }

        public int? PalletNumber { get; set; }

        public int LocationId { get; set; }

        public string ArticleNumber { get; set; }

        public string InvoiceDescription { get; set; }

        public int ConsignmentId { get; set; }

        public int Quantity { get; set; }

        public string Location { get; set; }

        public int QuantityOfItemsAtLocation { get; set; }

        public int QtyNeededFromLocation { get; set; }
    }
}
