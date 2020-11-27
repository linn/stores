namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ChangeRequestRepository : IQueryRepository<ChangeRequest>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ChangeRequestRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ChangeRequest FindBy(Expression<Func<ChangeRequest, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChangeRequest> FilterBy(Expression<Func<ChangeRequest, bool>> expression)
        {
            return this.serviceDbContext.ChangeRequests.Where(expression);
        }

        public IQueryable<ChangeRequest> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
