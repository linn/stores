namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;

    public class PackingListResponseProcessor : JsonResponseProcessor<PackingList>
    {
        public PackingListResponseProcessor(IResourceBuilder<PackingList> resourceBuilder)
            : base(resourceBuilder, "packing-list", 1)
        {
        }
    }
}
