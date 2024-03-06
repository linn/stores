namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class StoresTransactionDefinitionFacadeService : FacadeService<StoresTransactionDefinition, string, StoresTransactionDefinitionResource, StoresTransactionDefinitionResource>
    {
        public StoresTransactionDefinitionFacadeService(IRepository<StoresTransactionDefinition, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override StoresTransactionDefinition CreateFromResource(StoresTransactionDefinitionResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(StoresTransactionDefinition entity, StoresTransactionDefinitionResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StoresTransactionDefinition, bool>> SearchExpression(string searchTerm)
        {
            return def => (def.TransactionCode.ToUpper().Contains(searchTerm.ToUpper()) ||
                                                        def.Description.ToUpper().Contains(searchTerm.ToUpper()));
        }
    }
}
