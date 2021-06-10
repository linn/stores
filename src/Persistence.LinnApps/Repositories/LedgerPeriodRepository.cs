namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class LedgerPeriodRepository : IQueryRepository<LedgerPeriod>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LedgerPeriodRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public IQueryable<LedgerPeriod> FindAll()
        {
            throw new NotImplementedException();
        }

        public LedgerPeriod FindBy(Expression<Func<LedgerPeriod, bool>> expression)
        {
            return this.serviceDbContext.LedgerPeriods.FirstOrDefault(expression);
        }

        public IQueryable<LedgerPeriod> FilterBy(Expression<Func<LedgerPeriod, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
