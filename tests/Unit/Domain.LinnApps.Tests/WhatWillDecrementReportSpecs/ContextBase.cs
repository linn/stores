namespace Linn.Stores.Domain.LinnApps.Tests.WhatWillDecrementReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Reports;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WhatWillDecrementReportService Sut { get; set; }

        protected IProductionTriggerLevelsService ProductionTriggerLevelsService { get; private set; }

        protected IWwdPack WwdPack { get; private set; }

        protected IQueryRepository<WwdWork> WwdWorkRepository { get; private set; }

        protected IQueryRepository<WwdWorkDetail> WwdWorkDetailsRepository { get; private set; }

        protected IQueryRepository<ChangeRequest> ChangeRequestRepository { get; private set; }

        protected IRepository<Part, int> PartsRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductionTriggerLevelsService = Substitute.For<IProductionTriggerLevelsService>();
            this.WwdPack = Substitute.For<IWwdPack>();
            this.WwdWorkRepository = Substitute.For<IQueryRepository<WwdWork>>();
            this.WwdWorkDetailsRepository = Substitute.For<IQueryRepository<WwdWorkDetail>>();
            this.ChangeRequestRepository = Substitute.For<IQueryRepository<ChangeRequest>>();
            this.PartsRepository = Substitute.For<IRepository<Part, int>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new WhatWillDecrementReportService(
                this.ProductionTriggerLevelsService,
                this.WwdPack,
                this.WwdWorkRepository,
                this.WwdWorkDetailsRepository,
                this.ChangeRequestRepository,
                this.PartsRepository,
                this.ReportingHelper);
        }
    }
}
