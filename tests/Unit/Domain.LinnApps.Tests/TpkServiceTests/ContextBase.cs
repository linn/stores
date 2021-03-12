namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Tpk;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ITpkService Sut { get; set; }

        protected IQueryRepository<TransferableStock> TpkView { get; set; }

        protected IQueryRepository<AccountingCompany> AccountingCompaniesRepository { get; set; }

        protected ITpkPack TpkPack { get; set; }

        protected IBundleLabelPack BundleLabelPack { get; set; }

        protected IWhatToWandService WhatToWandService { get; set; }

        protected IStoresPack StoresPack { get; set; }

        protected IQueryRepository<SalesOutlet> SalesOutletRepository { get; set; }

        protected IQueryRepository<Consignment> ConsignmentRepository { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TpkView = Substitute.For<IQueryRepository<TransferableStock>>();
            this.AccountingCompaniesRepository = Substitute.For<IQueryRepository<AccountingCompany>>();
            this.TpkPack = Substitute.For<ITpkPack>();
            this.BundleLabelPack = Substitute.For<IBundleLabelPack>();
            this.WhatToWandService = Substitute.For<IWhatToWandService>();
            this.StoresPack = Substitute.For<IStoresPack>();
            this.SalesOutletRepository = Substitute.For<IQueryRepository<SalesOutlet>>();
            this.ConsignmentRepository = Substitute.For<IQueryRepository<Consignment>>();
            this.Sut = new TpkService(
                this.TpkView,
                this.AccountingCompaniesRepository,
                this.TpkPack,
                this.BundleLabelPack,
                this.WhatToWandService,
                this.SalesOutletRepository,
                this.StoresPack, 
                this.ConsignmentRepository);
        }
    }
}
