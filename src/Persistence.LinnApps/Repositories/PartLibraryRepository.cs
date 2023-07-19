namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartLibraryRepository : IRepository<PartLibrary, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartLibraryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartLibrary FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartLibrary> FindAll()
        {
            return this.serviceDbContext.PartLibraries;
        }

        public void Add(PartLibrary entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PartLibrary entity)
        {
            throw new NotImplementedException();
        }

        public PartLibrary FindBy(Expression<Func<PartLibrary, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartLibrary> FilterBy(Expression<Func<PartLibrary, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
