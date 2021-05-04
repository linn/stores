namespace Linn.Stores.Domain.LinnApps.Tests.TqmsReportsServiceTests
{
    using FluentAssertions.Extensions;

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

        protected IRepository<TqmsJobRef, string> TqmsJobRefsRepository { get; private set; }

        protected string JobRef { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TqmsSummaryByCategoryRepository = Substitute.For<IQueryRepository<TqmsSummaryByCategory>>();
            this.TqmsOutstandingLoansByCategoryRepository = Substitute.For<IQueryRepository<TqmsOutstandingLoansByCategory>>();
            this.TqmsJobRefsRepository = Substitute.For<IRepository<TqmsJobRef, string>>();
            this.ReportingHelper = new ReportingHelper();

            this.JobRef = "abc";
            this.TqmsJobRefsRepository.FindById(this.JobRef)
                .Returns(new TqmsJobRef { JobRef = this.JobRef, DateOfRun = 1.June(2024) });

            this.Sut = new TqmsReportsService(
                this.ReportingHelper,
                this.TqmsSummaryByCategoryRepository,
                this.TqmsOutstandingLoansByCategoryRepository,
                this.TqmsJobRefsRepository);
        }
    }
}
