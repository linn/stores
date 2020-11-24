namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ManufacturersResponseProcessor : JsonResponseProcessor<IEnumerable<Manufacturer>>
    {
        public ManufacturersResponseProcessor(IResourceBuilder<IEnumerable<Manufacturer>> resourceBuilder)
            : base(resourceBuilder, "manufacturers", 1)
        {
        }
    }
}
