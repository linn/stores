namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookExchangeRateResponseProcessor : JsonResponseProcessor<ImportBookExchangeRate>
    {
        public ImportBookExchangeRateResponseProcessor(IResourceBuilder<ImportBookExchangeRate> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-book-exchange-rate", 1)
        {
        }
    }
}