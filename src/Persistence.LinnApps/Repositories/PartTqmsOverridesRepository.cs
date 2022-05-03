namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartTqmsOverridesRepository : IRepository<PartTqmsOverride, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartTqmsOverridesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartTqmsOverride FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartTqmsOverride> FindAll()
        {
            return this.serviceDbContext.PartTqmsOverrides;
        }

        public void Add(PartTqmsOverride entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PartTqmsOverride entity)
        {
            throw new NotImplementedException();
        }

        public PartTqmsOverride FindBy(Expression<Func<PartTqmsOverride, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartTqmsOverride> FilterBy(Expression<Func<PartTqmsOverride, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
