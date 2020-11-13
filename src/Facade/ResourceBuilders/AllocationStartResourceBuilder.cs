namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    public class AllocationStartResourceBuilder : IResourceBuilder<AllocationResult>
    {
        public AllocationResource Build(AllocationResult allocationStart)
        {
            return new AllocationResource
                       {
                           Id = allocationStart.Id,
                           AllocationNotes = allocationStart.AllocationNotes,
                           SosNotes = allocationStart.SosNotes,
                           Links = this.BuildLinks(allocationStart).ToArray()
            };
        }

        public string GetLocation(AllocationResult allocationStart)
        {
            return $"/logistics/sos-alloc-heads/{allocationStart.Id}";
        }

        object IResourceBuilder<AllocationResult>.Build(AllocationResult allocationStart) => this.Build(allocationStart);

        private IEnumerable<LinkResource> BuildLinks(AllocationResult allocationStart)
        {
            yield return new LinkResource { Rel = "display-results", Href = this.GetLocation(allocationStart) };
        }
    }
}
