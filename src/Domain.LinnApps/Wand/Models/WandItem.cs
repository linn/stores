namespace Linn.Stores.Domain.LinnApps.Wand.Models
{
    public class WandItem
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

        public string AllWanded { get; set; } 

        public int BoxesPerProduct { get; set; }

        public string BoxesWanded { get; set; }

        public string TypeOfSerialNumber { get; set; }

        public string FunctionCode { get; set; }
    }
}
