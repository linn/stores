namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public class RequisitionActionResponseProcessor : JsonResponseProcessor<RequisitionActionResult>
    {
        public RequisitionActionResponseProcessor(IResourceBuilder<RequisitionActionResult> resourceBuilder)
            : base(resourceBuilder, "requisition-action", 1)
        {
        }
    }
}
