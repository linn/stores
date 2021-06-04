namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransportCodeResponseProcessor : JsonResponseProcessor<ImportBookTransportCode>
    {
        public ImportBookTransportCodeResponseProcessor(IResourceBuilder<ImportBookTransportCode> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-book-transport-code", 1)
        {
        }
    }
}