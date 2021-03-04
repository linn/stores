namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Tpk;

    public class TransferableStockRepository : IQueryRepository<TransferableStock>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TransferableStockRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TransferableStock FindBy(Expression<Func<TransferableStock, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TransferableStock> FilterBy(Expression<Func<TransferableStock, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TransferableStock> FindAll()
        {
            return this.serviceDbContext.TransferableStock;
        }
    }
}
