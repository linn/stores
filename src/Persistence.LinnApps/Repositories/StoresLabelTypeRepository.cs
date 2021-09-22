namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class StoresLabelTypeRepository : IQueryRepository<StoresLabelType>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresLabelTypeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresLabelType FindBy(Expression<Func<StoresLabelType, bool>> expression)
        {
            return this.serviceDbContext.StoresLabelTypes
                .Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<StoresLabelType> FilterBy(Expression<Func<StoresLabelType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresLabelType> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
