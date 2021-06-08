namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ConsignmentsResponseProcessor : JsonResponseProcessor<IEnumerable<Consignment>>
    {
        public ConsignmentsResponseProcessor(IResourceBuilder<IEnumerable<Consignment>> resourceBuilder)
            : base(resourceBuilder, "consignments", 1)
        {
        }
    }
}
