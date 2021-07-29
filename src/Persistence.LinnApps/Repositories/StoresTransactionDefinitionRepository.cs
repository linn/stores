namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class StoresTransactionDefinitionRepository 
        : IQueryRepository<StoresTransactionDefinition>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresTransactionDefinitionRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresTransactionDefinition FindBy(
            Expression<Func<StoresTransactionDefinition, bool>> expression)
        {
            return this.serviceDbContext
                .StoresTransactionDefinitions
                .Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<StoresTransactionDefinition> FilterBy(
            Expression<Func<StoresTransactionDefinition, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresTransactionDefinition> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
