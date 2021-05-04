namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;

    using Microsoft.EntityFrameworkCore;

    public class TqmsSummaryByCategoryRepository : IQueryRepository<TqmsSummaryByCategory>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TqmsSummaryByCategoryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TqmsSummaryByCategory FindBy(Expression<Func<TqmsSummaryByCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TqmsSummaryByCategory> FilterBy(Expression<Func<TqmsSummaryByCategory, bool>> expression)
        {
            return this.serviceDbContext
                .TqmsSummaryByCategories
                .AsNoTracking()
                .Where(expression);
        }

        public IQueryable<TqmsSummaryByCategory> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
