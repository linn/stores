namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class DespatchPickingSummaryRepository : IQueryRepository<DespatchPickingSummary>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DespatchPickingSummaryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public DespatchPickingSummary FindBy(Expression<Func<DespatchPickingSummary, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DespatchPickingSummary> FilterBy(Expression<Func<DespatchPickingSummary, bool>> expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<DespatchPickingSummary> IQueryRepository<DespatchPickingSummary>.FindAll()
        {
            return this.serviceDbContext.DespatchPickingSummary;
        }
    }
}
