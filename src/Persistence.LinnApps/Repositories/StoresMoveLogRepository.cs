namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StoresMoveLogRepository : IQueryRepository<StoresMoveLog>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresMoveLogRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresMoveLog FindBy(Expression<Func<StoresMoveLog, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresMoveLog> FilterBy(Expression<Func<StoresMoveLog, bool>> expression)
        {
            return this.serviceDbContext.StoresMoveLogs.Where(expression);
        }

        public IQueryable<StoresMoveLog> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
