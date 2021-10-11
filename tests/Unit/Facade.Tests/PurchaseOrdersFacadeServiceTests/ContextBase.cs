namespace Linn.Stores.Facade.Tests.PurchaseOrdersServiceTests
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IRepository<PurchaseOrder, int> PurchaseOrderRepository { get; private set; }

        protected IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource> Sut
        {
            get;
            private set;
        }
        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PurchaseOrderRepository = Substitute.For<IRepository<PurchaseOrder, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new PurchaseOrderFacadeService(this.PurchaseOrderRepository, this.TransactionManager);
        }
    }
}
