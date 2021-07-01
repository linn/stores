namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class SalesArticleService : ISalesArticleService
    {
        private readonly IQueryRepository<SalesArticle> repository;

        public SalesArticleService(IQueryRepository<SalesArticle> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<SalesArticle>> SearchLiveSalesArticles(string searchTerm)
        {
            return new SuccessResult<IEnumerable<SalesArticle>>(
                this.repository.FilterBy(x => x.PhaseOutDate == null 
                                            && x.ArticleNumber == searchTerm.ToUpper()));
        }
    }
}
