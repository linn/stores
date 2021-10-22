namespace Linn.Stores.Facade.Tests.MechPartSourceFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected MechPartSourceFacadeService Sut { get; private set; }

        protected IMechPartSourceService DomainService { get; private set; }

        protected IPartRepository PartRepository { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        protected IRepository<MechPartSource, int> MechPartSourceRepository { get; private set; }

        protected IQueryRepository<Supplier> SupplierRepository { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IRepository<Manufacturer, string> ManufacturerRepository { get; set; }

        protected IQueryRepository<RootProduct> RootProductRepository { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DomainService = Substitute.For<IMechPartSourceService>();
            this.PartRepository = Substitute.For<IPartRepository>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.MechPartSourceRepository = Substitute.For<IRepository<MechPartSource, int>>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.SupplierRepository = Substitute.For<IQueryRepository<Supplier>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.ManufacturerRepository = Substitute.For<IRepository<Manufacturer, string>>();
            this.RootProductRepository = Substitute.For<IQueryRepository<RootProduct>>();
            this.Sut = new MechPartSourceFacadeService(
                this.MechPartSourceRepository, 
                this.TransactionManager, 
                this.DomainService,
                this.PartRepository,
                this.DatabaseService,
                this.SupplierRepository,
                this.RootProductRepository,
                this.ManufacturerRepository,
                this.EmployeeRepository);
        }
    }
}
