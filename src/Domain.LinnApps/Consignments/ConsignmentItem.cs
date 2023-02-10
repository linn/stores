namespace Linn.Stores.Domain.LinnApps.Consignments
{
    public class ConsignmentItem
    {
        public int ConsignmentId { get; set; }

        public int ItemNumber { get; set; }

        public string ItemType { get; set; }

        public int? SerialNumber { get; set; }

        public decimal Quantity { get; set; }

        public string MaybeHalfAPair { get; set; }

        public decimal? Weight { get; set; }

        public int? Height { get; set; }

        public int? Depth { get; set; }

        public int? Width { get; set; }

        public string ContainerType { get; set; }

        public int? ContainerNumber { get; set; }

        public int? PalletNumber { get; set; }

        public int? OrderNumber { get; set; }

        public int? OrderLine { get; set; }

        public decimal? ItemBaseWeight { get; set; }

        public string ItemDescription { get; set; }

        public int? RsnNumber { get; set; }

        public Rsn Rsn { get; set; }

        public SalesOrder SalesOrder { get; set; }
    }
}
