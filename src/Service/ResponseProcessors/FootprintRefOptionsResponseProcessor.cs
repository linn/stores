namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class FootprintRefOptionsResponseProcessor : JsonResponseProcessor<IEnumerable<FootprintRefOption>>
    {
        public FootprintRefOptionsResponseProcessor(IResourceBuilder<IEnumerable<FootprintRefOption>> resourceBuilder)
            : base(resourceBuilder, "footprint-ref-options", 1)
        {
        }
    }
}
