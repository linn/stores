namespace Linn.Stores.Facade.Tests.ImportBookTransactionCodeServiceTests
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
        protected IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource> Sut { get; private set; }

        protected IRepository<ImportBookTransactionCode, int> ImportBookTransactionCodeRepository { get; private set; }
        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.ImportBookTransactionCodeRepository = Substitute.For<IRepository<ImportBookTransactionCode, int>>();
            this.Sut = new ImportBookTransactionCodeFacadeService(this.ImportBookTransactionCodeRepository, this.TransactionManager);
        }
    }
}
