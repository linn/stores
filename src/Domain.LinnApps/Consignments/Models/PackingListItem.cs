namespace Linn.Stores.Domain.LinnApps.Consignments.Models
{
    public class PackingListItem
    {
        public int ItemNumber { get; set; }

        public int? ContainerNumber { get; set; }

        public string Description { get; set; }
    }
}
