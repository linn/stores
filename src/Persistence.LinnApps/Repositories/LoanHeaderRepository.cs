namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class LoanHeaderRepository : IQueryRepository<LoanHeader>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LoanHeaderRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public IQueryable<LoanHeader> FilterBy(Expression<Func<LoanHeader, bool>> expression)
        {
            return this.serviceDbContext.LoanHeaders.Where(expression);
        }

        public IQueryable<LoanHeader> FindAll()
        {
            throw new NotImplementedException();
        }

        public LoanHeader FindBy(Expression<Func<LoanHeader, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
