namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class RequisitionResponseProcessor : JsonResponseProcessor<RequisitionHeader>
    {
        public RequisitionResponseProcessor(IResourceBuilder<RequisitionHeader> resourceBuilder)
            : base(resourceBuilder, "requisition", 1)
        {
        }
    }
}
