namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class HubRepository : IRepository<Hub, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public HubRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Hub FindById(int key)
        {
            return this.serviceDbContext.Hubs.Where(p => p.HubId == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Hub> FindAll()
        {
            return this.serviceDbContext.Hubs;
        }

        public void Add(Hub entity)
        {
            this.serviceDbContext.Hubs.Add(entity);
        }

        public void Remove(Hub entity)
        {
            throw new NotImplementedException();
        }

        public Hub FindBy(Expression<Func<Hub, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Hub> FilterBy(Expression<Func<Hub, bool>> expression)
        {
            return this.serviceDbContext
                .Hubs
                .Where(expression);
        }
    }
}
