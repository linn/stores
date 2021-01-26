namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class TqmsCategoriesRepository : IRepository<TqmsCategory, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TqmsCategoriesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TqmsCategory FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TqmsCategory> FindAll()
        {
            return this.serviceDbContext.TqmsCategories;
        }

        public void Add(TqmsCategory entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TqmsCategory entity)
        {
            throw new NotImplementedException();
        }

        public TqmsCategory FindBy(Expression<Func<TqmsCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TqmsCategory> FilterBy(Expression<Func<TqmsCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
