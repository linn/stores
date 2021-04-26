namespace Linn.Stores.Domain.LinnApps
{
    public class Consignment
    {
        public int ConsignmentId { get; set; }

        public int? SalesAccountId { get; set; }

        public int? AddressId { get; set; }

        public Country Country { get; set; } 

        public string CountryCode { get; set; }
    }
}
