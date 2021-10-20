namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class PurchaseLedgerRepository : IRepository<PurchaseLedger, int>
    {
        private readonly ServiceDbContext serviceDbContext;
        private readonly IPurchaseLedgerPack purchaseLedgerPack;


        public PurchaseLedgerRepository(ServiceDbContext serviceDbContext, IPurchaseLedgerPack purchaseLedgerPack)
        {
            this.serviceDbContext = serviceDbContext;
            this.purchaseLedgerPack = purchaseLedgerPack;
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
            var pltref = this.purchaseLedgerPack.GetNextLedgerSeq();
            entity.Pltref = pltref;

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
