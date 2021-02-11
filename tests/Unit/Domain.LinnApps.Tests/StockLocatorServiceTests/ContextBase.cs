namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IRepository<StockLocator, int> StockLocatorRepository { get; private set; }

        protected IStoresPalletRepository StoresPalletRepository { get; private set; }

        protected IQueryRepository<StoragePlace> StoragePlaceRepository { get; private set; }

        protected IRepository<StorageLocation, int> StorageLocationRepository { get; private set; }

        protected IQueryRepository<StockLocatorLocationsViewModel> StockLocatorLocationsView { get; private set; }

        protected IStockLocatorService Sut { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StockLocatorRepository = Substitute.For<IRepository<StockLocator, int>>();
            this.StoresPalletRepository = Substitute.For<IStoresPalletRepository>();
            this.StoragePlaceRepository = Substitute.For<IQueryRepository<StoragePlace>>();
            this.StorageLocationRepository = Substitute.For<IRepository<StorageLocation, int>>();
            this.StockLocatorLocationsView = Substitute.For<IQueryRepository<StockLocatorLocationsViewModel>>();
            this.AuthService = Substitute.For<IAuthorisationService>();
            this.Sut = new StockLocatorService(
                this.StockLocatorRepository, 
                this.StoresPalletRepository,
                this.StoragePlaceRepository,
                this.StorageLocationRepository,
                this.StockLocatorLocationsView,
                this.AuthService);
        }
    }
}
