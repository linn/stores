namespace Linn.Stores.Facade.Tests.StockTriggerLevelsServiceTests
{
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected StockTriggerLevelsFacadeService Sut { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected ILog LoggingService { get; private set; }

        protected IRepository<StockTriggerLevel, int> Repository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Repository = Substitute.For<IRepository<StockTriggerLevel, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.LoggingService = Substitute.For<ILog>();

            this.Sut = new StockTriggerLevelsFacadeService(
                this.Repository,
                this.TransactionManager,
                this.DatabaseService,
                this.LoggingService);
        }
    }
}
