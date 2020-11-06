namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class MechPartSourceRepository : IRepository<MechPartSource, int>
    {
        public MechPartSource FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MechPartSource> FindAll()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IQueryable<MechPartSource> FilterBy(Expression<Func<MechPartSource, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
