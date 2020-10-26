namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
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
                           SosNotes = allocationStart.SosNotes
                       };
        }

        public string GetLocation(AllocationStart allocationStart)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<AllocationStart>.Build(AllocationStart allocationStart) => this.Build(allocationStart);
    }
}