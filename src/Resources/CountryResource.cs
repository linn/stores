namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class CountryResource : HypermediaResource
    {
        public string CountryCode { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string TradeCurrency { get; set; }

        public string ECMember { get; set; }
    }
}
