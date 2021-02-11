namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class DespatchPalletQueueDetailsRepository : IQueryRepository<DespatchPalletQueueDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DespatchPalletQueueDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public DespatchPalletQueueDetail FindBy(Expression<Func<DespatchPalletQueueDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DespatchPalletQueueDetail> FilterBy(Expression<Func<DespatchPalletQueueDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<DespatchPalletQueueDetail> IQueryRepository<DespatchPalletQueueDetail>.FindAll()
        {
            return this.serviceDbContext.DespatchPalletQueueDetails;
        }
    }
}
