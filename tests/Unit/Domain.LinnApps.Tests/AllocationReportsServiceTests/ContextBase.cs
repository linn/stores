namespace Linn.Stores.Domain.LinnApps.Tests.AllocationReportsServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IAllocationReportsService Sut { get; private set; }

        protected IQueryRepository<DespatchPickingSummary> DespatchPickingSummaryRepository{ get; private set; }

        protected IQueryRepository<DespatchPalletQueueDetail> DespatchPalletQueueDetailRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DespatchPickingSummaryRepository = Substitute.For<IQueryRepository<DespatchPickingSummary>>();
            this.DespatchPalletQueueDetailRepository = Substitute.For<IQueryRepository<DespatchPalletQueueDetail>>();
            this.ReportingHelper = new ReportingHelper();

            this.Sut = new AllocationReportsService(
                this.DespatchPickingSummaryRepository,
                this.DespatchPalletQueueDetailRepository,
                this.ReportingHelper);
        }
    }
}
