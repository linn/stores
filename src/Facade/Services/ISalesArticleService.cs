namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface ISalesArticleService
    {
        IResult<IEnumerable<SalesArticle>> SearchLiveSalesArticles(string searchTerm);
    }
}
