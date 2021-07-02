namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class CartonTypesResponseProcessor : JsonResponseProcessor<IEnumerable<CartonType>>
    {
        public CartonTypesResponseProcessor(IResourceBuilder<IEnumerable<CartonType>> resourceBuilder)
            : base(resourceBuilder, "carton-types", 1)
        {
        }
    }
}
