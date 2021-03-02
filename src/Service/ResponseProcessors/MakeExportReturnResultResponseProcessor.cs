namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    // TODO delete
    public class MakeExportReturnResultResponseProcessor : JsonResponseProcessor<MakeExportReturnResult>
    {
        public MakeExportReturnResultResponseProcessor(IResourceBuilder<MakeExportReturnResult> resourceBuilder)
            : base(resourceBuilder, "make-export-return-result", 1)
        {
        }
    }
}