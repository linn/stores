namespace Linn.Stores.Facade.Tests.StockAvailableFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected StockAvailableFacadeService Sut { get; private set; }

        protected IQueryRepository<StockAvailable> StockAvailableRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StockAvailableRepository = Substitute.For<IQueryRepository<StockAvailable>>();
            this.Sut = new StockAvailableFacadeService(this.StockAvailableRepository);
        }
    }
}
