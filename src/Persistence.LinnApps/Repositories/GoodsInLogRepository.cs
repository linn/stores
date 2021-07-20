namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class GoodsInLogRepository : IRepository<GoodsInLogEntry, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public GoodsInLogRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Add(GoodsInLogEntry entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GoodsInLogEntry> FilterBy(Expression<Func<GoodsInLogEntry, bool>> expression)
        {
            throw new NotImplementedException();
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
