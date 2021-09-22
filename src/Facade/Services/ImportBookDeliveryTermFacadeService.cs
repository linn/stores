namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookDeliveryTermFacadeService : FacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource,
        ImportBookDeliveryTermResource>
    {
        public ImportBookDeliveryTermFacadeService(
            IRepository<ImportBookDeliveryTerm, string> deliveryTermRepository,
            ITransactionManager transactionManager)
            : base(deliveryTermRepository, transactionManager)
        {
        }

        protected override ImportBookDeliveryTerm CreateFromResource(ImportBookDeliveryTermResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ImportBookDeliveryTerm entity, ImportBookDeliveryTermResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ImportBookDeliveryTerm, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
