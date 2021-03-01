namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class SalesOutletRepository : IQueryRepository<SalesOutlet>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesOutletRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesOutlet FindBy(Expression<Func<SalesOutlet, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SalesOutlet> FilterBy(Expression<Func<SalesOutlet, bool>> expression)
        {
            return this.serviceDbContext.SalesOutlets.Where(expression).AsNoTracking();
        }

        public IQueryable<SalesOutlet> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}