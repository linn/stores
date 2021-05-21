namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ConsignmentShipfileResponseProcessor : JsonResponseProcessor<ConsignmentShipfile>
    {
        public ConsignmentShipfileResponseProcessor(IResourceBuilder<ConsignmentShipfile> resourceBuilder)
            : base(resourceBuilder, "consignment-shipfile", 1)
        {
        }
    }
}
