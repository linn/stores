namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;

    public class SosAllocHeadsResourceBuilder : IResourceBuilder<IEnumerable<SosAllocHead>>
    {
        private readonly SosAllocHeadResourceBuilder sosAllocHeadResourceBuilder = new SosAllocHeadResourceBuilder();

        public IEnumerable<SosAllocHeadResource> Build(IEnumerable<SosAllocHead> sosAllocHeads)
        {
            return sosAllocHeads
                .Select(a => this.sosAllocHeadResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<SosAllocHead>>.Build(IEnumerable<SosAllocHead> sosAllocHeads) => this.Build(sosAllocHeads);

        public string GetLocation(IEnumerable<SosAllocHead> sosAllocHeads)
        {
            throw new NotImplementedException();
        }
    }
}