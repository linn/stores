namespace Linn.Stores.Facade.Tests.ImportBookTransportCodeFacadeServiceTests
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource> Sut { get; private set; }

        protected IRepository<ImportBookTransportCode, int> ImportBookTransportCodeRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.ImportBookTransportCodeRepository = Substitute.For<IRepository<ImportBookTransportCode, int>>();
            this.Sut = new ImportBookTransportCodeFacadeService(
                this.ImportBookTransportCodeRepository,
                this.TransactionManager);
        }
    }
}
