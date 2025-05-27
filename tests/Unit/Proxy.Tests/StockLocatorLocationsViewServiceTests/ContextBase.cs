namespace Linn.Stores.Proxy.Tests.StockLocatorLocationsViewServiceTests
{
    using Linn.Common.Logging;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IStockLocatorLocationsViewService Sut { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected ILog Log { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Log = Substitute.For<ILog>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.Sut = new StockLocatorLocationsViewService(this.DatabaseService, this.Log);
        }
    }
}
