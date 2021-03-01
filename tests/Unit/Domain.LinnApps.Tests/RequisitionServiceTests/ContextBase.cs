namespace Linn.Stores.Domain.LinnApps.Tests.RequisitionServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IRequisitionService Sut { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected IRepository<RequisitionHeader, int> RequistionHeaderRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StoresPack = Substitute.For<IStoresPack>();
            this.RequistionHeaderRepository = Substitute.For<IRepository<RequisitionHeader, int>>();

            this.Sut = new RequisitionService(this.StoresPack, this.RequistionHeaderRepository);
        }
    }
}
