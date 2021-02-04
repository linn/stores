namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class InspectedStatesRepository : IRepository<InspectedState, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public InspectedStatesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public InspectedState FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<InspectedState> FindAll()
        {
            return this.serviceDbContext.InspectedStates;
        }

        public void Add(InspectedState entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(InspectedState entity)
        {
            throw new NotImplementedException();
        }

        public InspectedState FindBy(Expression<Func<InspectedState, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<InspectedState> FilterBy(Expression<Func<InspectedState, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
