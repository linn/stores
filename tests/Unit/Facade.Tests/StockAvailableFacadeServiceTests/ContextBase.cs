namespace Linn.Stores.Facade.Tests.StockAvailableFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AvailableStockFacadeService Sut { get; private set; }

        protected IQueryRepository<AvailableStock> StockAvailableRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StockAvailableRepository = Substitute.For<IQueryRepository<AvailableStock>>();
            this.Sut = new AvailableStockFacadeService(this.StockAvailableRepository);
        }
    }
}
