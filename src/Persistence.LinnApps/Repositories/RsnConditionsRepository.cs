namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RsnConditionsRepository : IQueryRepository<RsnCondition>
    {
        private readonly ServiceDbContext serviceDbContext;

        public RsnConditionsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public RsnCondition FindBy(Expression<Func<RsnCondition, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RsnCondition> FilterBy(Expression<Func<RsnCondition, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RsnCondition> FindAll()
        {
            return this.serviceDbContext.RsnConditions;
        }
    }
}
