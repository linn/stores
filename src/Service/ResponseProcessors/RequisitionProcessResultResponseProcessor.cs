namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class RequisitionProcessResultResponseProcessor : JsonResponseProcessor<RequisitionProcessResult>
    {
        public RequisitionProcessResultResponseProcessor(IResourceBuilder<RequisitionProcessResult> resourceBuilder)
            : base(resourceBuilder, "req-process-result", 1)
        {
        }
    }
}
