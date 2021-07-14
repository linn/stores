namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class SalesArticlesResourceBuilder
                    : IResourceBuilder<IEnumerable<SalesArticle>>
    {
        private readonly SalesArticleResourceBuilder salesArticleResourceBuilder = new SalesArticleResourceBuilder();

        public IEnumerable<SalesArticleResource> Build(IEnumerable<SalesArticle> salesArticles)
        {
            return salesArticles.Select(s => this.salesArticleResourceBuilder.Build(s));
        }

        public string GetLocation(IEnumerable<SalesArticle> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<SalesArticle>>.Build(IEnumerable<SalesArticle> salesArticles) =>
            this.Build(salesArticles);
    }
}
