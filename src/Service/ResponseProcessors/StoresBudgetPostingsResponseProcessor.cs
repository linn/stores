namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class StoresBudgetPostingsResponseProcessor : JsonResponseProcessor<IEnumerable<StoresBudgetPosting>>
    {
        public StoresBudgetPostingsResponseProcessor(IResourceBuilder<IEnumerable<StoresBudgetPosting>> resourceBuilder)
            : base(resourceBuilder, "stores-budget-posting", 1)
        {
        }
    }
}
