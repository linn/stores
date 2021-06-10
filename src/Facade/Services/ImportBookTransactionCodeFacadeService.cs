namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookTransactionCodeFacadeService : FacadeService<ImportBookTransactionCode, int,
        ImportBookTransactionCodeResource, ImportBookTransactionCodeResource>
    {
        public ImportBookTransactionCodeFacadeService(
            IRepository<ImportBookTransactionCode, int> transactionCodeRepository,
            ITransactionManager transactionManager)
            : base(transactionCodeRepository, transactionManager)
        {
        }

        protected override ImportBookTransactionCode CreateFromResource(ImportBookTransactionCodeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(
            ImportBookTransactionCode entity,
            ImportBookTransactionCodeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ImportBookTransactionCode, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
