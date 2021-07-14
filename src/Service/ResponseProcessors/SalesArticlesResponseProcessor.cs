namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class SalesArticlesResponseProcessor : JsonResponseProcessor<IEnumerable<SalesArticle>>
    {
        public SalesArticlesResponseProcessor(IResourceBuilder<IEnumerable<SalesArticle>> resourceBuilder)
            : base(resourceBuilder, "sales-articles", 1)
        {
        }
    }
}
