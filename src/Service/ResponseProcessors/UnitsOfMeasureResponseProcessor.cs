namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class UnitsOfMeasureResponseProcessor : JsonResponseProcessor<IEnumerable<UnitOfMeasure>>
    {
        public UnitsOfMeasureResponseProcessor(IResourceBuilder<IEnumerable<UnitOfMeasure>> resourceBuilder)
            : base(resourceBuilder, "units-of-measure", 1)
        {
        }
    }
}
