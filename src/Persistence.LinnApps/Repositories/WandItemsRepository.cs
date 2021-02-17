namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandItemsRepository : IQueryRepository<WandItem>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WandItemsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WandItem FindBy(Expression<Func<WandItem, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WandItem> FilterBy(Expression<Func<WandItem, bool>> expression)
        {
            return this.serviceDbContext.WandItems.Where(expression);
        }

        public IQueryable<WandItem> FindAll()
        {
            return this.serviceDbContext.WandItems;
        }
    }
}
