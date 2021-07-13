namespace Linn.Stores.Facade.Tests.PortFacadeServiceTests
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
        protected IFacadeService<Port, string, PortResource, PortResource> Sut { get; private set; }

        protected IRepository<Port, string> PortRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.PortRepository = Substitute.For<IRepository<Port, string>>();
            this.Sut = new PortFacadeService(
                this.PortRepository,
                this.TransactionManager);
        }
    }
}
