namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class RootProductRepository : IQueryRepository<RootProduct>
    {
        private readonly ServiceDbContext serviceDbContext;

        public RootProductRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public RootProduct FindBy(Expression<Func<RootProduct, bool>> expression)
        {
            return this.serviceDbContext.RootProducts
                .AsNoTracking().Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<RootProduct> FilterBy(Expression<Func<RootProduct, bool>> expression)
        {
            return this.serviceDbContext.RootProducts
                       .AsNoTracking().Where(expression);
        }

        public IQueryable<RootProduct> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
