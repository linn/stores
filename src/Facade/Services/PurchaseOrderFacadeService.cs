namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class PurchaseOrderFacadeService : FacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>
    {
        public PurchaseOrderFacadeService(
            IRepository<PurchaseOrder, int> purchaseOrderRepository,
            ITransactionManager transactionManager)
            : base(purchaseOrderRepository, transactionManager)
        {
        }

        protected override PurchaseOrder CreateFromResource(PurchaseOrderResource resource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PurchaseOrder, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PurchaseOrder entity, PurchaseOrderResource updateResource)
        {
            throw new NotImplementedException();
        }
    }
}
