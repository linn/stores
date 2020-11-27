namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class WwdWorkRepository : IQueryRepository<WwdWork>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WwdWorkRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WwdWork FindBy(Expression<Func<WwdWork, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WwdWork> FilterBy(Expression<Func<WwdWork, bool>> expression)
        {
            return this.serviceDbContext.WwdWorks.Where(expression);
        }

        public IQueryable<WwdWork> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
