namespace Linn.Stores.Facade.Tests.SosAllocHeadFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SosAllocHeadFacadeService Sut { get; private set; }

        protected IQueryRepository<SosAllocHead> SosAllocHeadRepository { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SosAllocHeadRepository = Substitute.For<IQueryRepository<SosAllocHead>>();
            this.Sut = new SosAllocHeadFacadeService(this.SosAllocHeadRepository);
        }
    }
}