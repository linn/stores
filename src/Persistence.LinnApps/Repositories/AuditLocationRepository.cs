namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class AuditLocationRepository : IQueryRepository<AuditLocation>
    {
        private ServiceDbContext serviceDbContext;

        public AuditLocationRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AuditLocation FindBy(Expression<Func<AuditLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AuditLocation> FilterBy(Expression<Func<AuditLocation, bool>> expression)
        {
            return this.serviceDbContext.AuditLocations.Where(expression);
        }

        public IQueryable<AuditLocation> FindAll()
        {
            return this.serviceDbContext.AuditLocations;
        }
    }
}