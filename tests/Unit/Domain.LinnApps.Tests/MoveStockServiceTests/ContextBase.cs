namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockMove;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IMoveStockService Sut { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected string PartNumber { get; set; }

        protected int? ReqNumber { get; set; }

        protected int Quantity { get; set; }

        protected string From { get; set; }

        protected string To { get; set; }

        protected int UserNumber { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StoresPack = Substitute.For<IStoresPack>();

            this.PartNumber = "part 1";
            this.Quantity = 2;
            this.UserNumber = 808;

            this.Sut = new MoveStockService(this.StoresPack);
        }
    }
}
