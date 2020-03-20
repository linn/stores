namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    public class AllocationStartResponseProcessor : JsonResponseProcessor<AllocationStart>
    {
        public AllocationStartResponseProcessor(IResourceBuilder<AllocationStart> resourceBuilder)
            : base(resourceBuilder, "allocation-details", 1)
        {
        }
    }
}