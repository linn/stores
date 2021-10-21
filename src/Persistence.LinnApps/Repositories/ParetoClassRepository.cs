namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ParetoClassRepository : IRepository<ParetoClass, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ParetoClassRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ParetoClass FindById(string key)
        {
            return this.serviceDbContext.ParetoClasses
                .Where(p => p.ParetoCode == key)
                .ToList().FirstOrDefault();
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
