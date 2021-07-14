namespace Linn.Stores.Facade.Tests.ImportBookCpcNumberFacadeServiceTests
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
        protected IFacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource, ImportBookCpcNumberResource> Sut { get; private set; }

        protected IRepository<ImportBookCpcNumber, int> ImportBookCpcNumberRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.ImportBookCpcNumberRepository = Substitute.For<IRepository<ImportBookCpcNumber, int>>();
            this.Sut = new ImportBookCpcNumberFacadeService(
                this.ImportBookCpcNumberRepository,
                this.TransactionManager);
        }
    }
}
