namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Workstation;

    public class TopUpListJobRefRepository : IRepository<TopUpListJobRef, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TopUpListJobRefRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TopUpListJobRef FindById(string key)
        {
            return this.serviceDbContext
                .TopUpListJobRefs
                .Where(p => p.JobRef == key)
                .ToList()
                .FirstOrDefault();
        }

        public IQueryable<TopUpListJobRef> FindAll()
        {
            return this.serviceDbContext.TopUpListJobRefs;
        }

        public void Add(TopUpListJobRef entity)
        {
            this.serviceDbContext.TopUpListJobRefs.Add(entity);
        }

        public void Remove(TopUpListJobRef entity)
        {
            throw new NotImplementedException();
        }

        public TopUpListJobRef FindBy(Expression<Func<TopUpListJobRef, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TopUpListJobRef> FilterBy(Expression<Func<TopUpListJobRef, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
