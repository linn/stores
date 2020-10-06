namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CountriesResourceBuilder : IResourceBuilder<IEnumerable<Country>>
    {
        private readonly CountryResourceBuilder countryResourceBuilder = new CountryResourceBuilder();

        public IEnumerable<CountryResource> Build(IEnumerable<Country> countries)
        {
            return countries
                .OrderBy(b => b.CountryCode)
                .Select(a => this.countryResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Country>>.Build(IEnumerable<Country> countries) => this.Build(countries);

        public string GetLocation(IEnumerable<Country> countries)
        {
            throw new NotImplementedException();
        }
    }
}