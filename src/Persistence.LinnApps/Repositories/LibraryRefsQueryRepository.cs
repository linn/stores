namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class LibraryRefsQueryRepository : IQueryRepository<LibraryRef>
    {
        private ServiceDbContext serviceDbContext;

        public LibraryRefsQueryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public LibraryRef FindBy(Expression<Func<LibraryRef, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LibraryRef> FilterBy(Expression<Func<LibraryRef, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LibraryRef> FindAll()
        {
            return this.serviceDbContext.LibraryRefs;
        }
    }
}
