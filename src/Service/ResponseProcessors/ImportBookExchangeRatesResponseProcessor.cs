namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookExchangeRatesResponseProcessor : JsonResponseProcessor<IEnumerable<ImportBookExchangeRate>>
    {
        public ImportBookExchangeRatesResponseProcessor(IResourceBuilder<IEnumerable<ImportBookExchangeRate>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books-exchange-rate", 1)
        {
        }
    }
}