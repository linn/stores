namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ExportReturnResponseProcessor : JsonResponseProcessor<ExportReturn>
    {
        public ExportReturnResponseProcessor(IResourceBuilder<ExportReturn> resourceBuilder)
            : base(resourceBuilder, "export-return", 1)
        {
        }
    }
}