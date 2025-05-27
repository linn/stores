namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using System;
    using System.Linq.Expressions;
    using System.Linq;

    public class DespatchPalletQueueScsDetailsRepository : IQueryRepository<DespatchPalletQueueScsDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DespatchPalletQueueScsDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public DespatchPalletQueueScsDetail FindBy(Expression<Func<DespatchPalletQueueScsDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DespatchPalletQueueScsDetail> FilterBy(Expression<Func<DespatchPalletQueueScsDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<DespatchPalletQueueScsDetail> IQueryRepository<DespatchPalletQueueScsDetail>.FindAll()
        {
            return this.serviceDbContext.DespatchPalletQueueScsDetails;
        }
    }
}
