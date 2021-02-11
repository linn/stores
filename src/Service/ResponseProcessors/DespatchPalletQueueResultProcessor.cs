namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    public class DespatchPalletQueueResultProcessor : JsonResponseProcessor<DespatchPalletQueueResult>
    {
        public DespatchPalletQueueResultProcessor(IResourceBuilder<DespatchPalletQueueResult> resourceBuilder)
            : base(resourceBuilder, "linnapps-despatch-pallet-queue-report", 1)
        {
        }
    }
}
