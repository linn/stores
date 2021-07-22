namespace Linn.Stores.Facade.Tests.ImportBookReportFacadeServiceTests
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingEuReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new EUSearchResource { FromDate = "01-Jan-2021", ToDate = "01-Jun-2021", EuResults = true };
            this.ReportService.GetEUReport(1.January(2021), 1.June(2021), true).Returns(
                new ResultsModel { ReportTitle = new NameModel("EU Import Books Report") });
            this.result = this.Sut.GetImpbookEuReport(resource);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ReportService.Received().GetEUReport(1.January(2021), 1.June(2021), true);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ResultsModel>>();
            var dataResult = ((SuccessResult<ResultsModel>)this.result).Data;
            dataResult.ReportTitle.DisplayValue.Should().Be("EU Import Books Report");
        }
    }
}
