namespace Linn.Stores.Domain.LinnApps.Tests.TqmsReportsServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Tqms;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ITqmsReportsService Sut { get; private set; }

        protected IQueryRepository<TqmsOutstandingLoansByCategory> TqmsOutstandingLoansByCategoryRepository { get; private set; }

        protected IQueryRepository<TqmsSummaryByCategory> TqmsSummaryByCategoryRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TqmsSummaryByCategoryRepository = Substitute.For<IQueryRepository<TqmsSummaryByCategory>>();
            this.TqmsOutstandingLoansByCategoryRepository = Substitute.For<IQueryRepository<TqmsOutstandingLoansByCategory>>();
            this.ReportingHelper = new ReportingHelper();

            this.Sut = new TqmsReportsService(
                this.ReportingHelper,
                this.TqmsSummaryByCategoryRepository,
                this.TqmsOutstandingLoansByCategoryRepository);
        }
    }
}
