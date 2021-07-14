namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookDeliveryTermsResponseProcessor : JsonResponseProcessor<IEnumerable<ImportBookDeliveryTerm>>
    {
        public ImportBookDeliveryTermsResponseProcessor(
            IResourceBuilder<IEnumerable<ImportBookDeliveryTerm>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books-delivery-terms", 1)
        {
        }
    }
}
