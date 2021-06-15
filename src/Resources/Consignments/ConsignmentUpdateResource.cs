namespace Linn.Stores.Resources.Consignments
{
    using Linn.Common.Resources;

    public class ConsignmentUpdateResource : HypermediaResource
    {
        public string Carrier { get; set; }

        public string ShippingMethod { get; set; }

        public string Terms { get; set; }

        public string DespatchLocationCode { get; set; }

        public int? HubId { get; set; }
    }
}
