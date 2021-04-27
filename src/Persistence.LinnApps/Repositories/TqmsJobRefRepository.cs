namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;

    public class TqmsJobRefRepository : IRepository<TqmsJobRef, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TqmsJobRefRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TqmsJobRef FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TqmsJobRef> FindAll()
        {
            return this.serviceDbContext.TqmsJobRefs;
        }

        public void Add(TqmsJobRef entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TqmsJobRef entity)
        {
            throw new NotImplementedException();
        }

        public TqmsJobRef FindBy(Expression<Func<TqmsJobRef, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TqmsJobRef> FilterBy(Expression<Func<TqmsJobRef, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
