namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class
        ImportBookTransactionCodesResponseProcessor : JsonResponseProcessor<IEnumerable<ImportBookTransactionCode>>
    {
        public ImportBookTransactionCodesResponseProcessor(
            IResourceBuilder<IEnumerable<ImportBookTransactionCode>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books-transaction-codes", 1)
        {
        }
    }
}
