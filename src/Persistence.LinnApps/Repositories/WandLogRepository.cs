namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand;

    public class WandLogRepository : IRepository<WandLog, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WandLogRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WandLog FindById(int key)
        {
            return this.serviceDbContext.WandLogs.Where(a => a.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<WandLog> FindAll()
        {
            return this.serviceDbContext.WandLogs;
        }

        public void Add(WandLog entity)
        {
            this.serviceDbContext.WandLogs.Add(entity);
        }

        public void Remove(WandLog entity)
        {
            throw new NotImplementedException();
        }

        public WandLog FindBy(Expression<Func<WandLog, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WandLog> FilterBy(Expression<Func<WandLog, bool>> expression)
        {
            return this.serviceDbContext.WandLogs.Where(expression);
        }
    }
}
