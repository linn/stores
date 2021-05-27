namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    public class PackingListItem
    {
        public string Pallet { get; set; }

        public int? Box { get; set; }

        public int? To { get; set; }

        public int? Count { get; set; }

        public string ContentsDescription { get; set; }
    }
}
