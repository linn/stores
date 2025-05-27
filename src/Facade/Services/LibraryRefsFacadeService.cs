namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class LibraryRefsFacadeService : FacadeService<LibraryRef, string, LibraryRefResource, LibraryRefResource>
    {
        public LibraryRefsFacadeService(IRepository<LibraryRef, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override LibraryRef CreateFromResource(LibraryRefResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(LibraryRef entity, LibraryRefResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<LibraryRef, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
