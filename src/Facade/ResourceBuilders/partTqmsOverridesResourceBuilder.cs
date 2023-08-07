namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTqmsOverridesResourceBuilder 
        : IResourceBuilder<IEnumerable<PartTqmsOverride>>
    {
        private readonly PartTqmsOverrideResourceBuilder rootProductResourceBuilder 
            = new PartTqmsOverrideResourceBuilder();

        public IEnumerable<PartTqmsOverrideResource> Build(
            IEnumerable<PartTqmsOverride> overrides)
        {
            return overrides
                .Select(a => this.rootProductResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartTqmsOverride>>.Build(
            IEnumerable<PartTqmsOverride> overrides) => this.Build(overrides);

        public string GetLocation(IEnumerable<PartTqmsOverride> overrides)
        {
            throw new NotImplementedException();
        }
    }
}
