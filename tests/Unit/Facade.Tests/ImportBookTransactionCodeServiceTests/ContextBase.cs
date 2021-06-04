namespace Linn.Stores.Facade.Tests.ImportBookTransactionCodeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IImportBookTransactionCodeService Sut { get; private set; }

        protected IRepository<ImportBookTransactionCode, int> ImportBookTransactionCodeRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ImportBookTransactionCodeRepository = Substitute.For<IRepository<ImportBookTransactionCode, int>>();
            this.Sut = new ImportBookTransactionCodeService(this.ImportBookTransactionCodeRepository);
        }
    }
}
