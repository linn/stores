namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class TqmsCategoriesResourceBuilder 
        : IResourceBuilder<IEnumerable<TqmsCategory>>
    {
        private readonly TqmsCategoryResourceBuilder rootProductResourceBuilder 
            = new TqmsCategoryResourceBuilder();

        public IEnumerable<TqmsCategoryResource> Build(
            IEnumerable<TqmsCategory> rootProducts)
        {
            return rootProducts
                .Select(a => this.rootProductResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<TqmsCategory>>.Build(
            IEnumerable<TqmsCategory> rootProducts) => this.Build(rootProducts);

        public string GetLocation(IEnumerable<TqmsCategory> rootProducts)
        {
            throw new NotImplementedException();
        }
    }
}
