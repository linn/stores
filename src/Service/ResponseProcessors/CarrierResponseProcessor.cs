namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class CarrierResponseProcessor : JsonResponseProcessor<Carrier>
    {
        public CarrierResponseProcessor(IResourceBuilder<Carrier> resourceBuilder)
            : base(resourceBuilder, "carrier", 1)
        {
        }
    }
}
