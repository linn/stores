namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandItemsResponseProcessor : JsonResponseProcessor<IEnumerable<WandItem>>
    {
        public WandItemsResponseProcessor(IResourceBuilder<IEnumerable<WandItem>> resourceBuilder)
            : base(resourceBuilder, "linnapps-wand-items", 1)
        {
        }
    }
}
