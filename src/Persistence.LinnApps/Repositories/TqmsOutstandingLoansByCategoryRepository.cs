namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;

    using Microsoft.EntityFrameworkCore;

    public class TqmsOutstandingLoansByCategoryRepository : IQueryRepository<TqmsOutstandingLoansByCategory>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TqmsOutstandingLoansByCategoryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TqmsOutstandingLoansByCategory FindBy(Expression<Func<TqmsOutstandingLoansByCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TqmsOutstandingLoansByCategory> FilterBy(Expression<Func<TqmsOutstandingLoansByCategory, bool>> expression)
        {
            return this.serviceDbContext
                .TqmsOutstandingLoansByCategories
                .AsNoTracking()
                .Where(expression);
        }

        public IQueryable<TqmsOutstandingLoansByCategory> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
