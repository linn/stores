namespace Linn.Stores.Facade.Tests.StockTriggerLevelsServiceTests
{
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
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

        protected IStockTriggerLevelsRepository Repository { get; private set; }

        protected IRepository<StorageLocation, int> StorageLocationRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Repository = Substitute.For<IStockTriggerLevelsRepository>();
            this.StorageLocationRepository = Substitute.For<IRepository<StorageLocation, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.LoggingService = Substitute.For<ILog>();

            this.Sut = new StockTriggerLevelsFacadeService(
                this.Repository,
                this.StorageLocationRepository,
                this.TransactionManager,
                this.DatabaseService,
                this.LoggingService);
        }
    }
}
