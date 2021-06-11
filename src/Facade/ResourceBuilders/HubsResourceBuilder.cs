namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class HubsResourceBuilder : IResourceBuilder<IEnumerable<Hub>>
    {
        private readonly HubResourceBuilder hubResourceBuilder = new HubResourceBuilder();

        public IEnumerable<HubResource> Build(IEnumerable<Hub> hubs)
        {
            return hubs
                .Select(a => this.hubResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Hub>>.Build(IEnumerable<Hub> hubs) => this.Build(hubs);

        public string GetLocation(IEnumerable<Hub> hubs)
        {
            throw new NotImplementedException();
        }
    }
}
