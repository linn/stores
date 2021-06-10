namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookExchangeRatesResourceBuilder : IResourceBuilder<IEnumerable<ImportBookExchangeRate>>
    {
        private readonly ImportBookExchangeRateResourceBuilder importBookExchangeRateResourceBuilder =
            new ImportBookExchangeRateResourceBuilder();

        public IEnumerable<ImportBookExchangeRateResource> Build(IEnumerable<ImportBookExchangeRate> exchangeRates)
        {
            return exchangeRates.OrderBy(b => b.BaseCurrency)
                .Select(a => this.importBookExchangeRateResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookExchangeRate>>.Build(IEnumerable<ImportBookExchangeRate> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookExchangeRate> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
