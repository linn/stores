namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookResponseProcessor : JsonResponseProcessor<ImportBook>
    {
        public ImportBookResponseProcessor(IResourceBuilder<ImportBook> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-book", 1)
        {
        }
    }
}