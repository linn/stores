namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookCpcNumbersResponseProcessor : JsonResponseProcessor<IEnumerable<ImportBookCpcNumber>>
    {
        public ImportBookCpcNumbersResponseProcessor(
            IResourceBuilder<IEnumerable<ImportBookCpcNumber>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books-cpc-numbers", 1)
        {
        }
    }
}
