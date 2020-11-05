namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class CarriersResponseProcessor : JsonResponseProcessor<IEnumerable<Carrier>>
    {
        public CarriersResponseProcessor(IResourceBuilder<IEnumerable<Carrier>> resourceBuilder)
            : base(resourceBuilder, "carriers", 1)
        {
        }
    }
}