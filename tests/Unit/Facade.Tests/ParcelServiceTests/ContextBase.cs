namespace Linn.Stores.Facade.Tests.ParcelFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Proxy;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase
    {
        protected ParcelService Sut { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected IRepository<Parcel, int> ParcelRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ParcelRepository = Substitute.For<IRepository<Parcel, int>>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new ParcelService(
                this.ParcelRepository,
                this.TransactionManager,
                this.DatabaseService);
        }
    }
}
