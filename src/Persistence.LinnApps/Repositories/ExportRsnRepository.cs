namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ExportRsnRepository : IQueryRepository<ExportRsn>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ExportRsnRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public IQueryable<ExportRsn> FilterBy(Expression<Func<ExportRsn, bool>> expression)
        {
            return this.serviceDbContext.ExportRsns.Where(expression).AsNoTracking();
        }

        public IQueryable<ExportRsn> FindAll()
        {
            throw new NotImplementedException();
        }

        public ExportRsn FindBy(Expression<Func<ExportRsn, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
