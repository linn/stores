﻿namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
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

        protected ITpkOoPack TpkOoPack { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TpkView = Substitute.For<IQueryRepository<TransferableStock>>();
            this.AccountingCompaniesRepository = Substitute.For<IQueryRepository<AccountingCompany>>();
            this.TpkOoPack = Substitute.For<ITpkOoPack>();
            this.Sut = new TpkService(this.TpkView, this.AccountingCompaniesRepository, this.TpkOoPack);
        }
    }
}