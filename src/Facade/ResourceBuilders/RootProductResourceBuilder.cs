namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.Parts;

    public class RootProductResourceBuilder : IResourceBuilder<RootProduct>
    {
        public RootProductResource Build(RootProduct rootProduct)
        {
            return new RootProductResource
            {
                Name = rootProduct.Name,
                Description = rootProduct.Description,
            };
        }

        public string GetLocation(RootProduct rootProduct)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<RootProduct>.Build(RootProduct rootProduct) => this.Build(rootProduct);

        private IEnumerable<LinkResource> BuildLinks(RootProduct rootProduct)
        {
            throw new NotImplementedException();
        }
    }
}
