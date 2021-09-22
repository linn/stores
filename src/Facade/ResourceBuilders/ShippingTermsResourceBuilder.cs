namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ShippingTermsResourceBuilder : IResourceBuilder<IEnumerable<ShippingTerm>>
    {
        private readonly ShippingTermResourceBuilder shippingTermResourceBuilder = new ShippingTermResourceBuilder();

        public IEnumerable<ShippingTermResource> Build(IEnumerable<ShippingTerm> shippingTerms)
        {
            return shippingTerms
                .Select(a => this.shippingTermResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ShippingTerm>>.Build(IEnumerable<ShippingTerm> shippingTerms) => this.Build(shippingTerms);

        public string GetLocation(IEnumerable<ShippingTerm> shippingTerms)
        {
            throw new NotImplementedException();
        }
    }
}
