namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ConsignmentResponseProcessor : JsonResponseProcessor<Consignment>
    {
        public ConsignmentResponseProcessor(IResourceBuilder<Consignment> resourceBuilder)
            : base(resourceBuilder, "consignment", 1)
        {
        }
    }
}
