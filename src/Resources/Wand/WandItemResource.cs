namespace Linn.Stores.Resources.Wand
{
    public class WandItemResource
    {
        public int ConsignmentId { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int Quantity { get; set; }

        public decimal? QuantityScanned { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public int RequisitionNumber { get; set; }

        public int RequisitionLine { get; set; }

        public string LinnBarCode { get; set; }

        public string CountryCode { get; set; }

        public bool AllWanded { get; set; }

        public int BoxesPerProduct { get; set; }
    }
}
