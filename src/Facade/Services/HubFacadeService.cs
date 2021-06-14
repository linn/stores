namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class HubFacadeService : FacadeService<Hub, int, HubResource, HubResource>
    {
        public HubFacadeService(IRepository<Hub, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Hub CreateFromResource(HubResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Hub entity, HubResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Hub, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
