namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ParetoClassRepository : IRepository<ParetoClass, string>
    {
        public ParetoClass FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ParetoClass> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ParetoClass entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ParetoClass entity)
        {
            throw new NotImplementedException();
        }

        public ParetoClass FindBy(Expression<Func<ParetoClass, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ParetoClass> FilterBy(Expression<Func<ParetoClass, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}