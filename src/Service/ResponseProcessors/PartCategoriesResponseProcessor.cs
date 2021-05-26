namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartCategoriesResponseProcessor : JsonResponseProcessor<IEnumerable<PartCategory>>
    {
        public PartCategoriesResponseProcessor(IResourceBuilder<IEnumerable<PartCategory>> resourceBuilder)
            : base(resourceBuilder, "part-categories", 1)
        {
        }
    }
}
