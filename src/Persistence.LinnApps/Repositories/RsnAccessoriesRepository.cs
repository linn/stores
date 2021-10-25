namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RsnAccessoriesRepository : IQueryRepository<RsnAccessory>
    {
        private readonly ServiceDbContext serviceDbContext;

        public RsnAccessoriesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public RsnAccessory FindBy(Expression<Func<RsnAccessory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RsnAccessory> FilterBy(Expression<Func<RsnAccessory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RsnAccessory> FindAll()
        {
            return this.serviceDbContext.RsnAccessories;
        }
    }
}
