namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    public class AllocationStartResourceBuilder : IResourceBuilder<AllocationStart>
    {
        public AllocationStartResource Build(AllocationStart allocationStart)
        {
            return new AllocationStartResource
                       {
                           Id = allocationStart.Id,
                           AllocationNotes = allocationStart.AllocationNotes,
                           SosNotes = allocationStart.SosNotes,
                           Links = this.BuildLinks(allocationStart).ToArray()
            };
        }

        public string GetLocation(AllocationStart allocationStart)
        {
            return $"/logistics/sos-alloc-heads/{allocationStart.Id}";
        }

        object IResourceBuilder<AllocationStart>.Build(AllocationStart allocationStart) => this.Build(allocationStart);

        private IEnumerable<LinkResource> BuildLinks(AllocationStart allocationStart)
        {
            yield return new LinkResource { Rel = "display-results", Href = this.GetLocation(allocationStart) };
        }
    }
}
