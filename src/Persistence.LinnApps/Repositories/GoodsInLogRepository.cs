namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    
    public class GoodsInLogRepository : IRepository<GoodsInLogEntry, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public GoodsInLogRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Add(GoodsInLogEntry entity)
        {
            this.serviceDbContext.GoodsInLog.Add(entity);
            this.serviceDbContext.SaveChanges();
        }

        public IQueryable<GoodsInLogEntry> FilterBy(Expression<Func<GoodsInLogEntry, bool>> expression)
        {
            return this.serviceDbContext.GoodsInLog.Where(expression);
        }

        public IQueryable<GoodsInLogEntry> FindAll()
        {
            throw new NotImplementedException();
        }

        public GoodsInLogEntry FindBy(Expression<Func<GoodsInLogEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public GoodsInLogEntry FindById(int key)
        {
            return this.serviceDbContext.GoodsInLog.Where(e => e.Id == key)
                .ToList().FirstOrDefault();
        }

        public void Remove(GoodsInLogEntry entity)
        {
            throw new NotImplementedException();
        }
    }
}
