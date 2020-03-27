namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class DespatchLocationsResourceBuilder : IResourceBuilder<IEnumerable<DespatchLocation>>
    {
        private readonly DespatchLocationResourceBuilder despatchLocationResourceBuilder = new DespatchLocationResourceBuilder();

        public IEnumerable<DespatchLocationResource> Build(IEnumerable<DespatchLocation> despatchLocations)
        {
            return despatchLocations
                .OrderBy(b => b.Sequence)
                .Select(a => this.despatchLocationResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<DespatchLocation>>.Build(IEnumerable<DespatchLocation> despatchLocations) => this.Build(despatchLocations);

        public string GetLocation(IEnumerable<DespatchLocation> despatchLocations)
        {
            throw new NotImplementedException();
        }
    }
}