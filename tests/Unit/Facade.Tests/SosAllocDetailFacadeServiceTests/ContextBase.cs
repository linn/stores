namespace Linn.Stores.Facade.Tests.SosAllocDetailFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SosAllocDetailFacadeService Sut { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IRepository<SosAllocDetail, int> Repository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Repository = Substitute.For<IRepository<SosAllocDetail, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new SosAllocDetailFacadeService(this.Repository, this.TransactionManager);
        }
    }
}
