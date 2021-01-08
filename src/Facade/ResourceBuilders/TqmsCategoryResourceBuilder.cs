namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class TqmsCategoryResourceBuilder : IResourceBuilder<TqmsCategory>
    {
        public TqmsCategoryResource Build(TqmsCategory cat)
        {
            return new TqmsCategoryResource
                       {
                           Name = cat.Name,
                           Description = cat.Description,
                       };
        }

        public string GetLocation(TqmsCategory rootProduct)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<TqmsCategory>.Build(TqmsCategory cat) => this.Build(cat);

        private IEnumerable<LinkResource> BuildLinks(TqmsCategory cat)
        {
            throw new NotImplementedException();
        }
    }
}
