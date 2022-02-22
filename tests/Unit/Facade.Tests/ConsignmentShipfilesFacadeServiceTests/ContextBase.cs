namespace Linn.Stores.Facade.Tests.ConsignmentShipfilesFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IConsignmentShipfileFacadeService Sut { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IRepository<ConsignmentShipfile, int> Repository { get; private set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; private set; }

        protected IConsignmentShipfileService DomainService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Repository = Substitute.For<IRepository<ConsignmentShipfile, int>>();
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();

            this.DomainService = Substitute.For<IConsignmentShipfileService>();
            this.Sut = new ConsignmentShipfileFacadeService(
                this.Repository,
                this.ConsignmentRepository,
                this.DomainService,
                this.TransactionManager);
        }
    }
}
