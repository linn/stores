namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBooksResponseProcessor : JsonResponseProcessor<IEnumerable<ImportBook>>
    {
        public ImportBooksResponseProcessor(IResourceBuilder<IEnumerable<ImportBook>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books", 1)
        {
        }
    }
}