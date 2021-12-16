namespace Linn.Stores.Resources
{
    public class SalesOutletResource
    {
        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public string Name { get; set; }

        public int SalesCustomerId { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string DateInvalid { get; set; }

        public string DispatchTerms { get; set; }

        public int OutletAddress { get; set; }
    }
}