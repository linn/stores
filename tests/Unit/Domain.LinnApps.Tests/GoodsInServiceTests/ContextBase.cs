namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IGoodsInService Sut { get; private set; }

        protected IGoodsInPack GoodsInPack { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected IPalletAnalysisPack PalletAnalysisPack { get; private set; }

        protected IRepository<Part, int> PartsRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.GoodsInPack = Substitute.For<IGoodsInPack>();
            this.StoresPack = Substitute.For<IStoresPack>();
            this.PalletAnalysisPack = Substitute.For<IPalletAnalysisPack>();
            this.PartsRepository = Substitute.For<IRepository<Part, int>>();
            this.Sut = new GoodsInService(
                this.GoodsInPack, 
                this.StoresPack, 
                this.PalletAnalysisPack, 
                this.PartsRepository);
        }
    }
}
