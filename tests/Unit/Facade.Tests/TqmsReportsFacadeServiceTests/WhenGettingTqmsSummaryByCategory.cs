namespace Linn.Stores.Facade.Tests.TqmsReportsFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTqmsSummaryByCategory : ContextBase
    {
        private ResultsModel resultsModel;

        private string jobRef;

        private IResult<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.jobRef = "DEF";
            this.resultsModel = new ResultsModel { ReportTitle = new NameModel("title") };
            this.TqmsReportsService.TqmsSummaryByCategoryReport(this.jobRef).Returns(this.resultsModel);
            this.results = this.Sut.GetTqmsSummaryByCategory(this.jobRef);
        }

        [Test]
        public void ShouldCallService()
        {
            this.TqmsReportsService.Received().TqmsSummaryByCategoryReport(this.jobRef);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<ResultsModel>>();
            var dataResult = ((SuccessResult<ResultsModel>)this.results).Data;
            dataResult.ReportTitle.DisplayValue.Should().Be("title");
        }
    }
}
