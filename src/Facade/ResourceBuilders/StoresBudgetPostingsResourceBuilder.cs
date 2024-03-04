namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StoresBudgetPostingsResourceBuilder : IResourceBuilder<IEnumerable<StoresBudgetPosting>>
    {
        private readonly StoresBudgetPostingResourceBuilder postingResourceBuilder = new StoresBudgetPostingResourceBuilder();

        public IEnumerable<StoresBudgetPostingResource> Build(IEnumerable<StoresBudgetPosting> postings)
        {
            return postings
                .OrderBy(b => b.BudgetId)
                .ThenBy(b => b.Sequence)
                .Select(a => this.postingResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StoresBudgetPosting>>.Build(IEnumerable<StoresBudgetPosting> postings) => this.Build(postings);

        public string GetLocation(IEnumerable<StoresBudgetPosting> postings)
        {
            throw new NotImplementedException();
        }
    }
}
