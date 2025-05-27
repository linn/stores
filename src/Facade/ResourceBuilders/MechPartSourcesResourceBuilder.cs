namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartSourcesResourceBuilder : IResourceBuilder<IEnumerable<MechPartSource>>
    {
        private readonly MechPartSourceResourceBuilder resourceBuilder = new MechPartSourceResourceBuilder();

        public IEnumerable<MechPartSourceResource> Build(IEnumerable<MechPartSource> models)
        {
            return models
                .OrderBy(b => b.PartNumber)
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<MechPartSource>>.Build(IEnumerable<MechPartSource> parts) => this.Build(parts);

        public string GetLocation(IEnumerable<MechPartSource> parts)
        {
            throw new NotImplementedException();
        }
    }
}
