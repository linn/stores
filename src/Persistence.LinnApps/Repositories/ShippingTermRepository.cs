namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ShippingTermRepository : IRepository<ShippingTerm, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ShippingTermRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ShippingTerm FindById(int key)
        {
            return this.serviceDbContext.ShippingTerms.Where(p => p.Id == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<ShippingTerm> FindAll()
        {
            return this.serviceDbContext.ShippingTerms.Where(c => c.DateInvalid == null);
        }

        public void Add(ShippingTerm entity)
        {
            this.serviceDbContext.ShippingTerms.Add(entity);
        }

        public void Remove(ShippingTerm entity)
        {
            throw new NotImplementedException();
        }

        public ShippingTerm FindBy(Expression<Func<ShippingTerm, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ShippingTerm> FilterBy(Expression<Func<ShippingTerm, bool>> expression)
        {
            return this.serviceDbContext
                .ShippingTerms
                .Where(expression);
        }
    }
}
