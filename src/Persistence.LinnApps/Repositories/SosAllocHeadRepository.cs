namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;

    using Microsoft.EntityFrameworkCore;

    public class SosAllocHeadRepository : IQueryRepository<SosAllocHead>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SosAllocHeadRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SosAllocHead FindBy(Expression<Func<SosAllocHead, bool>> expression)
        {
            return this.serviceDbContext.SosAllocHeads.Where(expression)
                .Include(a => a.SalesOutlet).ToList()
                .FirstOrDefault();
        }

        public IQueryable<SosAllocHead> FilterBy(Expression<Func<SosAllocHead, bool>> expression)
        {
            return this.serviceDbContext.SosAllocHeads.Where(expression).Include(a => a.SalesOutlet);
        }

        public IQueryable<SosAllocHead> FindAll()
        {
            return this.serviceDbContext.SosAllocHeads.AsNoTracking();
        }
    }
}
