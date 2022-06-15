namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IPartService Sut { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        protected IQueryRepository<Supplier> SupplierRepo { get; private set; }

        protected IRepository<QcControl, int> QcControlRepo { get; private set; }

        protected IPartRepository PartRepository { get; private set; }

        protected IRepository<MechPartSource, int> SourceRepository { get; private set; }

        protected IRepository<PartTemplate, string> TemplateRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IPartPack PartPack { get; private set; }

        protected IFilterByWildcardRepository<StockLocator, int> StockLocatorRepository { get; private set; }

        protected IRepository<PartDataSheet, PartDataSheetKey> DataSheetRepository { get; private set; }

        protected IDeptStockPartsService DeptStockPartsService { get; private set; }

        protected IEmailService EmailService { get; private set; }

        protected IQueryRepository<PhoneListEntry> PhoneList { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AuthService = Substitute.For<IAuthorisationService>();
            this.SupplierRepo = Substitute.For<IQueryRepository<Supplier>>();
            this.QcControlRepo = Substitute.For<IRepository<QcControl, int>>();
            this.PartRepository = Substitute.For<IPartRepository>();
            this.TemplateRepository = Substitute.For<IRepository<PartTemplate, string>>();
            this.SourceRepository = Substitute.For<IRepository<MechPartSource, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.StockLocatorRepository = Substitute.For<IFilterByWildcardRepository<StockLocator, int>>();
            this.PartPack = Substitute.For<IPartPack>();
            this.DataSheetRepository = Substitute.For<IRepository<PartDataSheet, PartDataSheetKey>>();
            this.DeptStockPartsService = Substitute.For<IDeptStockPartsService>();
            this.EmailService = Substitute.For<IEmailService>();
            this.PhoneList = Substitute.For<IQueryRepository<PhoneListEntry>>();

            this.Sut = new PartService(
                this.AuthService,
                this.QcControlRepo,
                this.SupplierRepo,
                this.PartRepository,
                this.TemplateRepository,
                this.SourceRepository,
                this.DataSheetRepository,
                this.PartPack,
                this.DeptStockPartsService,
                this.EmailService,
                this.PhoneList);
        }
    }
}
