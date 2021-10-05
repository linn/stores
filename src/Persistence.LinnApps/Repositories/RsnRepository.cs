namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Persistence;
    using Domain.LinnApps;
    using Microsoft.EntityFrameworkCore;

    public class RsnRepository : IQueryRepository<Rsn>
    {
        private readonly ServiceDbContext serviceDbContext;

        public RsnRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Rsn FindBy(Expression<Func<Rsn, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Rsn> FilterBy(Expression<Func<Rsn, bool>> expression)
        {
            return serviceDbContext.Rsns.Include(r => r.SalesArticle).ThenInclude(sa => sa.Tariff)
                .Where(expression).AsNoTracking();
        }

        public IQueryable<Rsn> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
