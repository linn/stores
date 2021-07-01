namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class SalesArticleResourceBuilder : IResourceBuilder<SalesArticle>
    {
        public SalesArticleResource Build(SalesArticle model)
        {
            return new SalesArticleResource
                       {
                           ArticleNumber = model.ArticleNumber,
                           Description = model.Description
                       };
        }

        object IResourceBuilder<SalesArticle>.Build(SalesArticle model) => this.Build(model);

        public string GetLocation(SalesArticle model)
        {
            throw new System.NotImplementedException();
        }
    }
}
