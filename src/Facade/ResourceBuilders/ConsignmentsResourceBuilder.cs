namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentsResourceBuilder : IResourceBuilder<IEnumerable<Consignment>>
    {
        private readonly ConsignmentResourceBuilder consignmentsResourceBuilder = new ConsignmentResourceBuilder();

        public IEnumerable<ConsignmentResource> Build(IEnumerable<Consignment> consignments)
        {
            return consignments.Select(consignment => this.consignmentsResourceBuilder.Build(consignment));
        }

        object IResourceBuilder<IEnumerable<Consignment>>.Build(IEnumerable<Consignment> consignments) => this.Build(consignments);

        public string GetLocation(IEnumerable<Consignment> consignments)
        {
            throw new NotImplementedException();
        }
    }
}
