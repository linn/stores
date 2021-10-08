namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class LoanRepository : IQueryRepository<Loan>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LoanRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public IQueryable<Loan> FilterBy(Expression<Func<Loan, bool>> expression)
        {
            return this.serviceDbContext.LoanHeaders.Where(expression);
        }

        public IQueryable<Loan> FindAll()
        {
            throw new NotImplementedException();
        }

        public Loan FindBy(Expression<Func<Loan, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
