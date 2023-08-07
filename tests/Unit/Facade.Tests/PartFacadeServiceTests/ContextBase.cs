namespace Linn.Stores.Facade.Tests.PartFacadeServiceTests
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
        protected IPartsFacadeService Sut { get; private set; }

        protected IPartRepository PartRepository { get; private set; }

        protected IRepository<ParetoClass, string> ParetoClassRepository { get; private set; }

        protected IRepository<AssemblyTechnology, string> AssemblyTechnologyRepository { get; private set; }

        protected IRepository<DecrementRule, string> DecrementRuleRepository { get; private set; }

        protected IQueryRepository<ProductAnalysisCode> ProductAnalysisCodeRepository { get; private set; }

        protected IQueryRepository<AccountingCompany> AccountingCompanyRepository { get; private set; }

        protected IQueryRepository<NominalAccount> NominalAccountRepository { get; private set; }

        protected IQueryRepository<SernosSequence> SernosSequenceRepository { get; private set; }

        protected IQueryRepository<Supplier> SupplierRepository { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected IPartService PartService { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.PartRepository = Substitute.For<IPartRepository>();
            this.ParetoClassRepository = Substitute.For<IRepository<ParetoClass, string>>();
            this.AssemblyTechnologyRepository = Substitute.For<IRepository<AssemblyTechnology, string>>();
            this.DecrementRuleRepository = Substitute.For<IRepository<DecrementRule, string>>();
            this.ProductAnalysisCodeRepository = Substitute.For<IQueryRepository<ProductAnalysisCode>>();
            this.AccountingCompanyRepository = Substitute.For<IQueryRepository<AccountingCompany>>();
            this.NominalAccountRepository = Substitute.For<IQueryRepository<NominalAccount>>();
            this.SernosSequenceRepository = Substitute.For<IQueryRepository<SernosSequence>>();
            this.SupplierRepository = Substitute.For<IQueryRepository<Supplier>>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.PartService= Substitute.For<IPartService>();
            this.Sut = new PartFacadeService(
                this.PartRepository,
                this.ParetoClassRepository,
                this.ProductAnalysisCodeRepository,
                this.AccountingCompanyRepository,
                this.NominalAccountRepository,
                this.AssemblyTechnologyRepository,
                this.DecrementRuleRepository,
                this.SernosSequenceRepository,
                this.SupplierRepository,
                this.EmployeeRepository,
                this.PartService,
                this.DatabaseService,
                this.TransactionManager);
        }
    }
}
