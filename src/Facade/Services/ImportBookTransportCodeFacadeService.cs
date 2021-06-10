namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookTransportCodeFacadeService : FacadeService<ImportBookTransportCode, int,
        ImportBookTransportCodeResource, ImportBookTransportCodeResource>
    {
        public ImportBookTransportCodeFacadeService(
            IRepository<ImportBookTransportCode, int> transportCodeRepository,
            ITransactionManager transactionManager)
            : base(transportCodeRepository, transactionManager)
        {
        }

        protected override ImportBookTransportCode CreateFromResource(ImportBookTransportCodeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(
            ImportBookTransportCode entity,
            ImportBookTransportCodeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ImportBookTransportCode, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
