namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTqmsOverrideResourceBuilder : IResourceBuilder<PartTqmsOverride>
    {
        public PartTqmsOverrideResource Build(PartTqmsOverride cat)
        {
            return new PartTqmsOverrideResource
                       {
                           Name = cat.Name,
                           Description = cat.Description,
                       };
        }

        public string GetLocation(PartTqmsOverride rootProduct)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartTqmsOverride>.Build(PartTqmsOverride cat) => this.Build(cat);

        private IEnumerable<LinkResource> BuildLinks(PartTqmsOverride cat)
        {
            throw new NotImplementedException();
        }
    }
}
