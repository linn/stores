namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartLibraryService : FacadeService<PartLibrary, string, PartLibraryResource, PartLibraryResource>
    {
        public PartLibraryService(IRepository<PartLibrary, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartLibrary CreateFromResource(PartLibraryResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartLibrary entity, PartLibraryResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartLibrary, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
