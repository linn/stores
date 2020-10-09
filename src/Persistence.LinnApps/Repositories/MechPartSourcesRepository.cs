namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class MechPartSourcesRepository : IRepository<MechPartSource, MechPartSourceKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MechPartSourcesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public MechPartSource FindById(MechPartSourceKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MechPartSource> FindAll()
        {
            return this.serviceDbContext.MechPartSources;
        }

        public void Add(MechPartSource entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(MechPartSource entity)
        {
            throw new NotImplementedException();
        }

        public MechPartSource FindBy(Expression<Func<MechPartSource, bool>> expression)
        {
            return this.serviceDbContext.MechPartSources.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<MechPartSource> FilterBy(Expression<Func<MechPartSource, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}