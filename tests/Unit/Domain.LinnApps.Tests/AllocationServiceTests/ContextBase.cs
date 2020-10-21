namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Sos;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IAllocationService Sut { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IRepository<SosOption, int> SosOptionRepository { get; private set; }

        protected ISosPack SosPack { get; private set; }

        protected IAllocPack AllocPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SosOptionRepository = Substitute.For<IRepository<SosOption, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.SosPack = Substitute.For<ISosPack>();
            this.AllocPack = Substitute.For<IAllocPack>();

            this.Sut = new AllocationService(
                this.SosPack,
                this.AllocPack,
                this.SosOptionRepository,
                this.TransactionManager);
        }
    }
}
