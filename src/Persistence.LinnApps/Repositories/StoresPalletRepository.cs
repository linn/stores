namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StoresPalletRepository : IRepository<StoresPallet, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresPalletRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresPallet FindById(int key)
        {
            return this.serviceDbContext.StoresPallets
                .Where(p => p.PalletNumber == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<StoresPallet> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(StoresPallet entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StoresPallet entity)
        {
            throw new NotImplementedException();
        }

        public StoresPallet FindBy(Expression<Func<StoresPallet, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresPallet> FilterBy(Expression<Func<StoresPallet, bool>> expression)
        {
            return this.serviceDbContext.StoresPallets.Where(expression);
        }
    }
}
