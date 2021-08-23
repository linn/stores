namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IGoodsInService Sut { get; private set; }

        protected IGoodsInPack GoodsInPack { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected IPalletAnalysisPack PalletAnalysisPack { get; private set; }

        protected IRepository<Part, int> PartsRepository { get; private set; }

        protected IRepository<GoodsInLogEntry, int> GoodsInLog { get; private set; }

        protected IRepository<RequisitionHeader, int> ReqRepository { get; private set; }

        protected IPurchaseOrderPack PurchaseOrderPack { get; private set; }

        protected IQueryRepository<StoresLabelType> LabelTypeRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.GoodsInPack = Substitute.For<IGoodsInPack>();
            this.StoresPack = Substitute.For<IStoresPack>();
            this.PalletAnalysisPack = Substitute.For<IPalletAnalysisPack>();
            this.PartsRepository = Substitute.For<IRepository<Part, int>>();
            this.GoodsInLog = Substitute.For<IRepository<GoodsInLogEntry, int>>();
            this.ReqRepository = Substitute.For<IRepository<RequisitionHeader, int>>();
            this.PurchaseOrderPack = Substitute.For<IPurchaseOrderPack>();
            this.LabelTypeRepository = Substitute.For<IQueryRepository<StoresLabelType>>();
            this.Sut = new GoodsInService(
                this.GoodsInPack, 
                this.StoresPack, 
                this.PalletAnalysisPack, 
                this.PartsRepository,
                this.GoodsInLog,
                this.ReqRepository,
                this.PurchaseOrderPack,
                this.LabelTypeRepository);
        }
    }
}
