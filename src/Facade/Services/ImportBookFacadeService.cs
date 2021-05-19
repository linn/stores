namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookFacadeService : FacadeService<ImportBook, int, ImportBookResource, ImportBookResource>
    {
        public ImportBookFacadeService(IRepository<ImportBook, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ImportBook CreateFromResource(ImportBookResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ImportBook entity, ImportBookResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ImportBook, bool>> SearchExpression(string searchTerm)
        {
            return imps => imps.Id.ToString().Contains(searchTerm);
        }
    }
}