namespace Linn.Stores.Facade.Tests.ImportBookReportFacadeServiceTests
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingIprReportInvalidDate : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new IPRSearchResource { FromDate = "01-Jan-2021", ToDate = "potato" };
            this.ReportService.GetIPRReport(1.January(2021), 1.June(2021), true).Returns(
                new ResultsModel { ReportTitle = new NameModel("IPR Import Books Consignments") });
            this.result = this.Sut.GetImpbookIPRReport(resource);
        }

        [Test]
        public void ShouldNotGetReport()
        {
            this.ReportService.DidNotReceiveWithAnyArgs().GetIPRReport(Arg.Any<DateTime>(), Arg.Any<DateTime>(), true);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ResultsModel>>();
        }
    }
}
