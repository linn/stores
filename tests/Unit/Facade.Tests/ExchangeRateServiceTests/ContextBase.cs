namespace Linn.Stores.Facade.Tests.ExchangeRateServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase
    {
        protected ImportBookExchangeRateService Sut { get; private set; }

        protected IImportBookService ImportBookService { get; private set; }

        protected IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> ExchangeRateRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ExchangeRateRepository = Substitute.For<IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey>>();
            this.ImportBookService = Substitute.For<IImportBookService>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new ImportBookExchangeRateService(
                this.ExchangeRateRepository, this.ImportBookService);
        }
    }
}
