namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class partTqmsOverridesResourceBuilder 
        : IResourceBuilder<IEnumerable<PartTqmsOverride>>
    {
        private readonly PartTqmsOverrideResourceBuilder rootProductResourceBuilder 
            = new PartTqmsOverrideResourceBuilder();

        public IEnumerable<PartTqmsOverrideResource> Build(
            IEnumerable<PartTqmsOverride> rootProducts)
        {
            return rootProducts
                .Select(a => this.rootProductResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartTqmsOverride>>.Build(
            IEnumerable<PartTqmsOverride> rootProducts) => this.Build(rootProducts);

        public string GetLocation(IEnumerable<PartTqmsOverride> rootProducts)
        {
            throw new NotImplementedException();
        }
    }
}
