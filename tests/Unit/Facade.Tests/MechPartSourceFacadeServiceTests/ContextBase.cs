namespace Linn.Stores.Facade.Tests.MechPartSourceFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Proxy;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected MechPartSourceFacadeService Sut { get; private set; }

        protected IMechPartSourceService DomainService { get; private set; }

        protected IRepository<Part, int> PartRepository { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        protected IRepository<MechPartSource, int> MechPartSourceRepository { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DomainService = Substitute.For<IMechPartSourceService>();
            this.PartRepository = Substitute.For<IRepository<Part, int>>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.MechPartSourceRepository = Substitute.For<IRepository<MechPartSource, int>>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new MechPartSourceFacadeService(
                this.MechPartSourceRepository, 
                this.TransactionManager, 
                this.DomainService,
                this.PartRepository,
                this.DatabaseService,
                this.EmployeeRepository);
        }
    }
}
