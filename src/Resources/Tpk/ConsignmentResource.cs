namespace Linn.Stores.Resources.Tpk
{
    public class ConsignmentResource
    {
        public int ConsignmentId { get; set; }

        public int? SalesAccountId { get; set; }

        public int? AddressId { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }
    }
}
