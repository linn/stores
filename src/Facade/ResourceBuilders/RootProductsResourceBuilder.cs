namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.Parts;

    public class RootProductsResourceBuilder : IResourceBuilder<IEnumerable<RootProduct>>
    {
        private readonly RootProductResourceBuilder rootProductResourceBuilder = new RootProductResourceBuilder();

        public IEnumerable<RootProductResource> Build(IEnumerable<RootProduct> rootProducts)
        {
            return rootProducts
                .Select(a => this.rootProductResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<RootProduct>>.Build(IEnumerable<RootProduct> rootProducts) => this.Build(rootProducts);

        public string GetLocation(IEnumerable<RootProduct> rootProducts)
        {
            throw new NotImplementedException();
        }
    }
}
