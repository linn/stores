namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AllocationFacadeService Sut { get; private set; }

        protected IAllocationService AllocationService { get;  private set; }

        protected IAllocationReportsService AllocationReportsService { get;  private set; }

        protected IQueryRepository<DespatchPalletQueueScsDetail> DpqRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AllocationService = Substitute.For<IAllocationService>();
            this.AllocationReportsService = Substitute.For<IAllocationReportsService>();
            this.DpqRepository = Substitute.For<IQueryRepository<DespatchPalletQueueScsDetail>>();
            this.Sut = new AllocationFacadeService(this.AllocationService, this.AllocationReportsService, this.DpqRepository);
        }
    }
}
