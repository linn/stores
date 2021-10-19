namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class PurchaseLedgerRepository : IRepository<PurchaseLedger, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PurchaseLedgerRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PurchaseLedger FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PurchaseLedger> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PurchaseLedger entity)
        {
            this.serviceDbContext.PurchaseLedgers.Add(entity);
        }

        public void Remove(PurchaseLedger entity)
        {
            throw new NotImplementedException();
        }

        public PurchaseLedger FindBy(Expression<Func<PurchaseLedger, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PurchaseLedger> FilterBy(Expression<Func<PurchaseLedger, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
