namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wcs;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IWarehouseService Sut { get; private set; }

        protected IWcsPack WcsPack { get; private set; }

        protected IQueryRepository<WarehouseLocation> WarehouseLocationRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WcsPack = Substitute.For<IWcsPack>();

            this.WarehouseLocationRepository = Substitute.For<IQueryRepository<WarehouseLocation>>();

            this.Sut = new WarehouseService(this.WcsPack, this.WarehouseLocationRepository);
        }
    }
}
