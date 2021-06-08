namespace Linn.Stores.Resources.Consignments
{
    using Linn.Common.Resources;

    public class ConsignmentResource : HypermediaResource
    {
        public int ConsignmentId { get; set; }

        public int? SalesAccountId { get; set; }

        public int? AddressId { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }
    }
}
