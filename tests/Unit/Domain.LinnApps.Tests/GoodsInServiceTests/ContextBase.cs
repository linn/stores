namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IGoodsInService Sut { get; private set; }

        protected IGoodsInPack GoodsInPack { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected IPalletAnalysisPack PalletAnalysisPack { get; private set; }

        protected IPartRepository PartsRepository { get; private set; }

        protected IRepository<GoodsInLogEntry, int> GoodsInLog { get; private set; }

        protected IRepository<RequisitionHeader, int> ReqRepository { get; private set; }

        protected IPurchaseOrderPack PurchaseOrderPack { get; private set; }

        protected IQueryRepository<StoresLabelType> LabelTypeRepository { get; private set; }

        protected IBartenderLabelPack Bartender { get; private set; }

        protected IRepository<PurchaseOrder, int> PurchaseOrderRepository { get; private set; }

        protected IQueryRepository<AuthUser> AuthUserRepository { get; private set; }

        protected IQueryRepository<StoragePlace> StoragePlaceRepository { get; private set; }

        protected IPrintRsnService PrintRsnService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.GoodsInPack = Substitute.For<IGoodsInPack>();
            this.StoresPack = Substitute.For<IStoresPack>();
            this.PalletAnalysisPack = Substitute.For<IPalletAnalysisPack>();
            this.PartsRepository = Substitute.For<IPartRepository>();
            this.GoodsInLog = Substitute.For<IRepository<GoodsInLogEntry, int>>();
            this.ReqRepository = Substitute.For<IRepository<RequisitionHeader, int>>();
            this.PurchaseOrderPack = Substitute.For<IPurchaseOrderPack>();
            this.LabelTypeRepository = Substitute.For<IQueryRepository<StoresLabelType>>();
            this.Bartender = Substitute.For<IBartenderLabelPack>();
            this.PurchaseOrderRepository = Substitute.For<IRepository<PurchaseOrder, int>>();
            this.AuthUserRepository = Substitute.For<IQueryRepository<AuthUser>>();
            this.StoragePlaceRepository = Substitute.For<IQueryRepository<StoragePlace>>();
            this.PrintRsnService = Substitute.For<IPrintRsnService>();
            this.StoragePlaceRepository.FindBy(Arg.Any<Expression<Func<StoragePlace, bool>>>())
                .Returns(new StoragePlace());

            this.Sut = new GoodsInService(
                this.GoodsInPack, 
                this.StoresPack, 
                this.PalletAnalysisPack, 
                this.PartsRepository,
                this.GoodsInLog,
                this.ReqRepository,
                this.PurchaseOrderPack,
                this.LabelTypeRepository,
                this.Bartender,
                this.PurchaseOrderRepository,
                this.AuthUserRepository,
                this.PrintRsnService,
                this.StoragePlaceRepository);
        }
    }
}
