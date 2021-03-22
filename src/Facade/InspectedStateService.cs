namespace Linn.Stores.Facade
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class InspectedStateService : FacadeService<InspectedState, string, InspectedStateResource, InspectedStateResource>
    {
        public InspectedStateService(IRepository<InspectedState, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override InspectedState CreateFromResource(InspectedStateResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(InspectedState entity, InspectedStateResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<InspectedState, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
