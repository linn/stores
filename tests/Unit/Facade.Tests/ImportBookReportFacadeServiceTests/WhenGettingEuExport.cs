namespace Linn.Stores.Facade.Tests.ImportBookReportFacadeServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingEuExport : ContextBase
    {
        private IResult<IEnumerable<IEnumerable<string>>> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new EUSearchResource { FromDate = "01-Jan-2021", ToDate = "01-Jun-2021", EuResults = true};
            this.ReportService.GetEUExport(1.January(2021), 1.June(2021), true).Returns(
                new ResultsModel { ReportTitle = new NameModel("EU Import Books Consignments") });
            this.result = this.Sut.GetImpbookEuReportExport(resource);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ReportService.Received().GetEUExport(1.January(2021), 1.June(2021), true);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<IEnumerable<string>>>>();
            var dataResult = ((SuccessResult<IEnumerable<IEnumerable<string>>>)this.result).Data;
        }
    }
}
