namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class SalesArticleRepository : IQueryRepository<SalesArticle>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesArticleRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesArticle FindBy(Expression<Func<SalesArticle, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SalesArticle> FilterBy(Expression<Func<SalesArticle, bool>> expression)
        {
            return this.serviceDbContext.SalesArticles.Where(expression).AsNoTracking();
        }

        public IQueryable<SalesArticle> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
