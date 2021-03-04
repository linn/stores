namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandResultResponseProcessor : JsonResponseProcessor<WandResult>
    {
        public WandResultResponseProcessor(IResourceBuilder<WandResult> resourceBuilder)
            : base(resourceBuilder, "linnapps-wand-result", 1)
        {
        }
    }
}
