namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CountryResourceBuilder : IResourceBuilder<Country>
    {
        public CountryResource Build(Country country)
        {
            return new CountryResource
                       {
                           CountryCode = country.CountryCode,
                           Name = country.Name,
                           DisplayName = country.DisplayName,
                           TradeCurrency = country.TradeCurrency,
                           ECMember = country.ECMember,
                           Links = this.BuildLinks(country).ToArray()
                       };
        }

        public string GetLocation(Country country)
        {
            return $"/logistics/countries/{country.CountryCode}";
        }

        object IResourceBuilder<Country>.Build(Country country) => this.Build(country);

        private IEnumerable<LinkResource> BuildLinks(Country country)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(country) };
        }
    }
}
