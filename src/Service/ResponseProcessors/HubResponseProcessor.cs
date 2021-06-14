namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class HubResponseProcessor : JsonResponseProcessor<Hub>
    {
        public HubResponseProcessor(IResourceBuilder<Hub> resourceBuilder)
            : base(resourceBuilder, "hub", 1)
        {
        }
    }
}
