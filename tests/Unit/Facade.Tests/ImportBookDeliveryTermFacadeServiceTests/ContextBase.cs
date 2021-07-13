namespace Linn.Stores.Facade.Tests.ImportBookDeliveryTermFacadeServiceTests
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
        protected IFacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource, ImportBookDeliveryTermResource> Sut { get; private set; }

        protected IRepository<ImportBookDeliveryTerm, string> ImportBookDeliveryTermRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.ImportBookDeliveryTermRepository = Substitute.For<IRepository<ImportBookDeliveryTerm, string>>();
            this.Sut = new ImportBookDeliveryTermFacadeService(
                this.ImportBookDeliveryTermRepository,
                this.TransactionManager);
        }
    }
}
