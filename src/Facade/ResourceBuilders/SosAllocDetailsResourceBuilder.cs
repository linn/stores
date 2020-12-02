namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class SosAllocDetailsResourceBuilder : IResourceBuilder<IEnumerable<SosAllocDetail>>
    {
        private readonly SosAllocDetailResourceBuilder sosAllocDetailResourceBuilder = new SosAllocDetailResourceBuilder();

        public IEnumerable<SosAllocDetailResource> Build(IEnumerable<SosAllocDetail> sosAllocDetails)
        {
            return sosAllocDetails
                .Select(a => this.sosAllocDetailResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<SosAllocDetail>>.Build(IEnumerable<SosAllocDetail> sosAllocDetails) => this.Build(sosAllocDetails);

        public string GetLocation(IEnumerable<SosAllocDetail> sosAllocDetails)
        {
            throw new NotImplementedException();
        }
    }
}
