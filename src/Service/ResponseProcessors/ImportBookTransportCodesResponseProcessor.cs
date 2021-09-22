namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransportCodesResponseProcessor : JsonResponseProcessor<IEnumerable<ImportBookTransportCode>>
    {
        public ImportBookTransportCodesResponseProcessor(
            IResourceBuilder<IEnumerable<ImportBookTransportCode>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books-transport-codes", 1)
        {
        }
    }
}
