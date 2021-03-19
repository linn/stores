namespace Linn.Stores.Facade.Tests.MoveStockFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps.StockMove;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected MoveStockFacadeService Sut { get; private set; }

        protected IMoveStockService MoveStockService { get; private set; }
        [SetUp]
        public void SetUpContext()
        {
            this.MoveStockService = Substitute.For<IMoveStockService>();
            this.Sut = new MoveStockFacadeService(this.MoveStockService);
        }
    }
}
