namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CurrenciesResourceBuilder : IResourceBuilder<IEnumerable<Currency>>
    {
        private readonly CurrencyResourceBuilder countryResourceBuilder = new CurrencyResourceBuilder();

        public IEnumerable<CurrencyResource> Build(IEnumerable<Currency> currencies)
        {
            return currencies
                .OrderBy(b => b.Code)
                .Select(a => this.countryResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Currency>>.Build(IEnumerable<Currency> currencies) => this.Build(currencies);

        public string GetLocation(IEnumerable<Currency> currencies)
        {
            throw new NotImplementedException();
        }
    }
}
