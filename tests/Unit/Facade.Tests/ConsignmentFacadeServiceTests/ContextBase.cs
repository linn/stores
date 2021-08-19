namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ConsignmentFacadeService Sut { get; private set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IConsignmentService ConsignmentService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.ConsignmentService = Substitute.For<IConsignmentService>();
            this.Sut = new ConsignmentFacadeService(
                this.ConsignmentRepository,
                this.TransactionManager,
                this.ConsignmentService);
        }
    }
}
