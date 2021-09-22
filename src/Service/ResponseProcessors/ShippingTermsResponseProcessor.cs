namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ShippingTermsResponseProcessor : JsonResponseProcessor<IEnumerable<ShippingTerm>>
    {
        public ShippingTermsResponseProcessor(IResourceBuilder<IEnumerable<ShippingTerm>> resourceBuilder)
            : base(resourceBuilder, "shipping-terms", 1)
        {
        }
    }
}
