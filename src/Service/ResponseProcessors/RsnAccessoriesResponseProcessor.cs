namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RsnAccessoriesResponseProcessor : JsonResponseProcessor<IEnumerable<RsnAccessory>>
    {
        public RsnAccessoriesResponseProcessor(IResourceBuilder<IEnumerable<RsnAccessory>> resourceBuilder)
            : base(resourceBuilder, "rsn-accessories", 1)
        {
        }
    }
}
