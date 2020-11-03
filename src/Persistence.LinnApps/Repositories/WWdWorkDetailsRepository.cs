namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class WwdWorkDetailsRepository : IQueryRepository<WwdWorkDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WwdWorkDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WwdWorkDetail FindBy(Expression<Func<WwdWorkDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WwdWorkDetail> FilterBy(Expression<Func<WwdWorkDetail, bool>> expression)
        {
            return this.serviceDbContext.WwdWorkDetails.Where(expression);
        }

        public IQueryable<WwdWorkDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}