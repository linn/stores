namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IRepository<StockLocator, int> StockLocatorRepository { get; private set; }

        protected IStoresPalletRepository StoresPalletRepository { get; private set; }

        protected IQueryRepository<StoragePlace> StoragePlaceRepository { get; private set; }

        protected IRepository<StorageLocation, int> StorageLocationRepository { get; private set; }

        protected IQueryRepository<StockLocatorLocation> StockLocatorLocationsView { get; private set; }

        protected IQueryRepository<StockLocatorBatch> StockLocatorBatchesView { get; private set; }

        protected IRepository<Part, int> PartRepository { get; private set; }

        protected IStockLocatorService Sut { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        protected IStockLocatorLocationsViewService LocationsViewService { get; private set; }

        protected IQueryRepository<StockLocatorPrices> StockLocatorView { get; private set; }

        protected IQueryRepository<ReqMove> ReqMoveRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StockLocatorRepository = Substitute.For<IRepository<StockLocator, int>>();
            this.StoresPalletRepository = Substitute.For<IStoresPalletRepository>();
            this.StoragePlaceRepository = Substitute.For<IQueryRepository<StoragePlace>>();
            this.StorageLocationRepository = Substitute.For<IRepository<StorageLocation, int>>();
            this.StockLocatorLocationsView = Substitute.For<IQueryRepository<StockLocatorLocation>>();
            this.StockLocatorBatchesView = Substitute.For<IQueryRepository<StockLocatorBatch>>();
            this.PartRepository = Substitute.For<IRepository<Part, int>>();
            this.AuthService = Substitute.For<IAuthorisationService>();
            this.LocationsViewService = Substitute.For<IStockLocatorLocationsViewService>();
            this.StockLocatorView = Substitute.For<IQueryRepository<StockLocatorPrices>>();
            this.ReqMoveRepository = Substitute.For<IQueryRepository<ReqMove>>();
            this.ReqMoveRepository.FindBy(Arg.Any<Expression<Func<ReqMove, bool>>>()).ReturnsNull();
            this.Sut = new StockLocatorService(
                this.StockLocatorRepository,
                this.StoresPalletRepository,
                this.StoragePlaceRepository,
                this.StorageLocationRepository,
                this.StockLocatorBatchesView,
                this.AuthService,
                this.LocationsViewService,
                this.StockLocatorView,
                this.PartRepository,
                this.ReqMoveRepository);
        }
    }
}
