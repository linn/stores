namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ExportRsnResponseProcessor : JsonResponseProcessor<ExportRsn>
    {
        public ExportRsnResponseProcessor(IResourceBuilder<ExportRsn> resourceBuilder)
            : base(resourceBuilder, "export-rsn", 1)
        {
        }
    }
}