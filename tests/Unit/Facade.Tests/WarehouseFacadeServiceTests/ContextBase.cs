namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WarehouseFacadeService Sut { get; private set; }

        protected IWarehouseService WarehouseService { get;  private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WarehouseService = Substitute.For<IWarehouseService>();
            this.Sut = new WarehouseFacadeService(this.WarehouseService);
        }
    }
}
