namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    public class WandConsignmentsResourceBuilder : IResourceBuilder<IEnumerable<WandConsignment>>
    {
        public IEnumerable<WandConsignmentResource> Build(IEnumerable<WandConsignment> wandConsignments)
        {
            return wandConsignments
                .Select(w => new WandConsignmentResource
                                 {
                                     ConsignmentId = w.ConsignmentId,
                                     Addressee = w.Addressee,
                                     CountryCode = w.CountryCode,
                                     IsDone = w.IsDone
                                 });
        }

        public string GetLocation(IEnumerable<WandConsignment> model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<WandConsignment>>.Build(IEnumerable<WandConsignment> wandConsignments) =>
            this.Build(wandConsignments);
    }
}
