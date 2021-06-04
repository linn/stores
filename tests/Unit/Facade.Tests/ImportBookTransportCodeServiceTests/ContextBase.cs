namespace Linn.Stores.Facade.Tests.ImportBookTransportCodeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IImportBookTransportCodeService Sut { get; private set; }

        protected IRepository<ImportBookTransportCode, int> ImportBookTransportCodeRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ImportBookTransportCodeRepository = Substitute.For<IRepository<ImportBookTransportCode, int>>();
            this.Sut = new ImportBookTransportCodeService(this.ImportBookTransportCodeRepository);
        }
    }
}
