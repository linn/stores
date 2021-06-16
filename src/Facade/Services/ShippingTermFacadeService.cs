namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ShippingTermFacadeService : FacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource>
    {
        public ShippingTermFacadeService(IRepository<ShippingTerm, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ShippingTerm CreateFromResource(ShippingTermResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ShippingTerm entity, ShippingTermResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ShippingTerm, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
