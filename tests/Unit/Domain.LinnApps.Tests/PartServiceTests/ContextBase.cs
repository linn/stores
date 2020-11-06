namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IPartService Sut { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        protected IQueryRepository<Supplier> SupplierRepo { get; private set; }

        protected IRepository<QcControl, int> QcControlRepo { get; private set; }

        protected IRepository<Part, string> PartRepository { get; private set; }

        protected IRepository<PartTemplate, string> TemplateRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IPartPack PartPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AuthService = Substitute.For<IAuthorisationService>();
            this.SupplierRepo = Substitute.For<IQueryRepository<Supplier>>();
            this.QcControlRepo = Substitute.For<IRepository<QcControl, int>>();
            this.PartRepository = Substitute.For<IRepository<Part, string>>();
            this.TemplateRepository = Substitute.For<IRepository<PartTemplate, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.PartPack = Substitute.For<IPartPack>();
            this.Sut = new PartService(
                this.AuthService,
                this.QcControlRepo,
                this.SupplierRepo,
                this.PartRepository,
                this.TemplateRepository,
                this.PartPack);
        }
    }
}