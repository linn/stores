namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class SupplierRepository : IQueryRepository<Supplier>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SupplierRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Supplier FindBy(Expression<Func<Supplier, bool>> expression)
        {
            return this.serviceDbContext.Suppliers.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<Supplier> FilterBy(Expression<Func<Supplier, bool>> expression)
        {
            return this.serviceDbContext.Suppliers.Where(expression);
        }

        public IQueryable<Supplier> FindAll()
        {
            return this.serviceDbContext.Suppliers.AsNoTracking();
        }
    }
}
