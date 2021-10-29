namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RsnConditionsResponseProcessor : JsonResponseProcessor<IEnumerable<RsnCondition>>
    {
        public RsnConditionsResponseProcessor(IResourceBuilder<IEnumerable<RsnCondition>> resourceBuilder)
            : base(resourceBuilder, "rsn-conditions", 1)
        {
        }
    }
}
