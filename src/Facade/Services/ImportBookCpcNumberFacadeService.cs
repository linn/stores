namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookCpcNumberFacadeService : FacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource,
        ImportBookCpcNumberResource>
    {
        public ImportBookCpcNumberFacadeService(
            IRepository<ImportBookCpcNumber, int> cpcNumberRepository,
            ITransactionManager transactionManager)
            : base(cpcNumberRepository, transactionManager)
        {
        }

        protected override ImportBookCpcNumber CreateFromResource(ImportBookCpcNumberResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(
            ImportBookCpcNumber entity,
            ImportBookCpcNumberResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ImportBookCpcNumber, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
