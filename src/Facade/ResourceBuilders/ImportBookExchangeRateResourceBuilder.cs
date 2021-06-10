namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookExchangeRateResourceBuilder : IResourceBuilder<ImportBookExchangeRate>
    {
        public ImportBookExchangeRateResource Build(ImportBookExchangeRate model)
        {
            return new ImportBookExchangeRateResource
            {
                PeriodNumber = model.PeriodNumber,
                BaseCurrency = model.BaseCurrency,
                ExchangeCurrency = model.ExchangeCurrency,
                ExchangeRate = model.ExchangeRate
            };
        }

        object IResourceBuilder<ImportBookExchangeRate>.Build(ImportBookExchangeRate model) => this.Build(model);

        public string GetLocation(ImportBookExchangeRate model)
        {
            throw new System.NotImplementedException();
        }
    }
}
