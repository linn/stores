namespace Linn.Stores.Facade.Tests.ImportBookFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ImportBookFacadeService Sut { get; private set; }

        protected IRepository<ImportBook, int> ImportBookRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IImportBookService DomainService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ImportBookRepository = Substitute.For<IRepository<ImportBook, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.DomainService = Substitute.For<IImportBookService>();
            this.Sut = new ImportBookFacadeService(
                this.ImportBookRepository,
                this.TransactionManager,
                this.DomainService);
        }
    }
}
