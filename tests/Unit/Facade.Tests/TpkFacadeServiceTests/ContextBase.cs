namespace Linn.Stores.Facade.Tests.TpkFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IQueryRepository<TransferableStock> Repository { get; private set; }

        protected ITpkService DomainService { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected ITpkFacadeService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Repository = Substitute.For<IQueryRepository<TransferableStock>>();
            this.DomainService = Substitute.For<ITpkService>();
            this.StoresPack = Substitute.For<IStoresPack>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new TpkFacadeService(
                this.Repository,
                this.DomainService,
                this.StoresPack,
                this.TransactionManager);
        }
    }
}
