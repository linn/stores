namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ShippingTermResponseProcessor : JsonResponseProcessor<ShippingTerm>
    {
        public ShippingTermResponseProcessor(IResourceBuilder<ShippingTerm> resourceBuilder)
            : base(resourceBuilder, "shipping-term", 1)
        {
        }
    }
}
