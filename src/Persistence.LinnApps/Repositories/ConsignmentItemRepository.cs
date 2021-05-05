namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    // todo - this could be redundant now that you've changed how to get all this data
    public class ConsignmentItemRepository : IRepository<ConsignmentItem, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ConsignmentItemRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ConsignmentItem FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ConsignmentItem> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ConsignmentItem entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ConsignmentItem entity)
        {
            throw new NotImplementedException();
        }

        public ConsignmentItem FindBy(Expression<Func<ConsignmentItem, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ConsignmentItem> FilterBy(Expression<Func<ConsignmentItem, bool>> expression)
        {
            return this.serviceDbContext.ConsignmentItems.Where(expression);
        }
    }
}
