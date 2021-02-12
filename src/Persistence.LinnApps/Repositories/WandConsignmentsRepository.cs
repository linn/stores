namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandConsignmentsRepository : IQueryRepository<WandConsignment>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WandConsignmentsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WandConsignment FindBy(Expression<Func<WandConsignment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WandConsignment> FilterBy(Expression<Func<WandConsignment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<WandConsignment> IQueryRepository<WandConsignment>.FindAll()
        {
            return this.serviceDbContext.WandConsignments;
        }
    }
}
