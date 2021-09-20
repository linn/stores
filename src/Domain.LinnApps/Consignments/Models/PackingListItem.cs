namespace Linn.Stores.Domain.LinnApps.Consignments.Models
{
    public class PackingListItem
    {
        public int ItemNumber { get; set; }

        public int? ContainerNumber { get; set; }

        public string Description { get; set; }

        public decimal? Volume { get; set; }

        public decimal? Weight { get; set; }

        public string DisplayDimensions { get; set; }
    }
}
