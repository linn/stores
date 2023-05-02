namespace Linn.Stores.Domain.LinnApps.Tests.StoresMoveLogReportTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Reports;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase
    {
        protected StoresMoveLogReportService Sut { get; set; }

        protected IQueryRepository<StoresMoveLog> StoresMoveLogRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StoresMoveLogRepository = Substitute.For<IQueryRepository<StoresMoveLog>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new StoresMoveLogReportService(
                this.StoresMoveLogRepository,
                this.ReportingHelper);
        }
    }
}
