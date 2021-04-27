namespace Linn.Stores.Facade.Tests.TqmsReportsFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTqmsSummaryByCategory : ContextBase
    {
        private ResultsModel resultsModel;

        private string jobRef;

        private IResult<IEnumerable<ResultsModel>> results;

        private TqmsSummaryRequestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.jobRef = "DEF";
            this.requestResource = new TqmsSummaryRequestResource { JobRef = this.jobRef, HeadingsOnly = false };
            this.resultsModel = new ResultsModel { ReportTitle = new NameModel("title") };
            this.TqmsReportsService.TqmsSummaryByCategoryReport(this.jobRef, false)
                .Returns(new List<ResultsModel> { this.resultsModel });
            this.results = this.Sut.GetTqmsSummaryByCategory(this.requestResource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.TqmsReportsService.Received().TqmsSummaryByCategoryReport(this.jobRef, false);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ResultsModel>>>();
            var dataResult = ((SuccessResult<IEnumerable<ResultsModel>>)this.results).Data;
            dataResult.First().ReportTitle.DisplayValue.Should().Be("title");
        }
    }
}
