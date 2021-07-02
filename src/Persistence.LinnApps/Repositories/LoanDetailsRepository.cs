namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class LoanDetailsRepository : IQueryRepository<LoanDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LoanDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public LoanDetail FindBy(Expression<Func<LoanDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LoanDetail> FilterBy(Expression<Func<LoanDetail, bool>> expression)
        {
            return this.serviceDbContext.LoanDetails.Where(expression);
        }

        public IQueryable<LoanDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
