namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class LedgerPeriodRepository : IRepository<LedgerPeriod, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LedgerPeriodRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public LedgerPeriod FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LedgerPeriod> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(LedgerPeriod entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(LedgerPeriod entity)
        {
            throw new NotImplementedException();
        }

        public LedgerPeriod FindBy(Expression<Func<LedgerPeriod, bool>> expression)
        {
            return this.serviceDbContext.LedgerPeriods.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<LedgerPeriod> FilterBy(Expression<Func<LedgerPeriod, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
