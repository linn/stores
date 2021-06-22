namespace Linn.Stores.Resources.Consignments
{
    using System.Collections.Generic;

    public class ConsignmentUpdateResource
    {
        public string Carrier { get; set; }

        public string ShippingMethod { get; set; }

        public string Terms { get; set; }

        public string DespatchLocationCode { get; set; }

        public int? HubId { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public string CustomsEntryCode { get; set; }

        public string CustomsEntryCodeDate { get; set; }

        public IEnumerable<ConsignmentPalletResource> Pallets { get; set; } = new List<ConsignmentPalletResource>();

        public IEnumerable<ConsignmentItemResource> Items { get; set; } = new List<ConsignmentItemResource>();
    }
}
