namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class NominalRepository : IQueryRepository<Nominal>
    {
        private readonly ServiceDbContext serviceDbContext;

        public NominalRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Nominal FindBy(Expression<Func<Nominal, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Nominal> FilterBy(Expression<Func<Nominal, bool>> expression)
        {
            return this.serviceDbContext.Nominals.Where(expression);
        }

        public IQueryable<Nominal> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}