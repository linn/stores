namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class LibraryRefsRepository : IRepository<LibraryRef, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LibraryRefsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public LibraryRef FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LibraryRef> FindAll()
        {
            return this.serviceDbContext.LibraryRefs;
        }

        public void Add(LibraryRef entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(LibraryRef entity)
        {
            throw new NotImplementedException();
        }

        public LibraryRef FindBy(Expression<Func<LibraryRef, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LibraryRef> FilterBy(Expression<Func<LibraryRef, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
