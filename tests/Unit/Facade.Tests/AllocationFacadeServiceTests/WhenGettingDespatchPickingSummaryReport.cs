namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDespatchPickingSummaryReport : ContextBase
    {
        private ResultsModel resultsModel;

        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.resultsModel = new ResultsModel { ReportTitle = new NameModel("title") };

            this.AllocationReportsService.DespatchPickingSummary()
                .Returns(this.resultsModel);

            this.result = this.Sut.DespatchPickingSummaryReport();
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationReportsService.Received().DespatchPickingSummary();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ResultsModel>>();
            var dataResult = ((SuccessResult<ResultsModel>)this.result).Data;
            dataResult.ReportTitle.DisplayValue.Should().Be(this.resultsModel.ReportTitle.DisplayValue);
        }
    }
}
