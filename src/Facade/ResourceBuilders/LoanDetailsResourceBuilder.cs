namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class LoanDetailsResourceBuilder : IResourceBuilder<IEnumerable<LoanDetail>>
    {
        private readonly LoanDetailResourceBuilder resourceBuilder = new LoanDetailResourceBuilder();

        public IEnumerable<LoanDetailResource> Build(IEnumerable<LoanDetail> entities)
        {
            return entities
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<LoanDetail>>.Build(IEnumerable<LoanDetail> entities) => this.Build(entities);

        public string GetLocation(IEnumerable<LoanDetail> entities)
        {
            throw new NotImplementedException();
        }
    }
}
