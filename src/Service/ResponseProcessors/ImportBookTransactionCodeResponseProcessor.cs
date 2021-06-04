namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransactionCodeResponseProcessor : JsonResponseProcessor<ImportBookTransactionCode>
    {
        public ImportBookTransactionCodeResponseProcessor(IResourceBuilder<ImportBookTransactionCode> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-book-transaction-code", 1)
        {
        }
    }
}