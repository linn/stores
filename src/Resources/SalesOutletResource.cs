namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    public class SalesOutletResource
    {
        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public string Name { get; set; }

        public int SalesCustomerId { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<RsnResource> Rsns { get; set; }
    }
}