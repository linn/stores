namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using Linn.Common.Persistence;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Microsoft.EntityFrameworkCore;

    public class StoresTransactionDefinitionRepository : IRepository<StoresTransactionDefinition, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresTransactionDefinitionRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresTransactionDefinition FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresTransactionDefinition> FindAll()
        {
            return this.serviceDbContext.StoresTransactionDefinitions;
        }

        public void Add(StoresTransactionDefinition entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StoresTransactionDefinition entity)
        {
            throw new NotImplementedException();
        }

        public StoresTransactionDefinition FindBy(Expression<Func<StoresTransactionDefinition, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresTransactionDefinition> FilterBy(Expression<Func<StoresTransactionDefinition, bool>> expression)
        {
            return this.serviceDbContext.StoresTransactionDefinitions.AsNoTracking().Where(expression).AsNoTracking();
        }
    }
}
