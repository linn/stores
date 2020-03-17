namespace Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Domain.LinnApps.Parts;

    using Linn.Common.Persistence;

    public class PartRepository : IRepository<Part, string>
    {
        public Part FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Part> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Part entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Part entity)
        {
            throw new NotImplementedException();
        }

        public Part FindBy(Expression<Func<Part, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Part> FilterBy(Expression<Func<Part, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}