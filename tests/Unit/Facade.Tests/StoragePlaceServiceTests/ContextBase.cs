namespace Linn.Stores.Facade.Tests.StoragePlaceServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected StoragePlaceService Sut { get; private set; }

        protected IStoragePlaceAuditPack StoragePlaceAuditPack { get; private set; }

        protected IQueryRepository<StoragePlace> StoragePlaceRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StoragePlaceRepository = Substitute.For<IQueryRepository<StoragePlace>>();
            this.StoragePlaceAuditPack = Substitute.For<IStoragePlaceAuditPack>();
            this.Sut = new StoragePlaceService(
                this.StoragePlaceRepository, 
                this.StoragePlaceAuditPack);
        }
    }
}
