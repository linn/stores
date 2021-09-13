namespace Linn.Stores.Resources.Consignments
{
    using System.Collections.Generic;

    public class PackingListResource
    {
        public int ConsignmentId { get; set; }

        public string DeliveryAddress { get; set; }

        public string SenderAddress { get; set; }

        public string DespatchDate { get; set; }

        public IEnumerable<PackingListItemResource> Items { get; set; }

        public IEnumerable<PackingListPalletResource> Pallets { get; set; }
    }
}
