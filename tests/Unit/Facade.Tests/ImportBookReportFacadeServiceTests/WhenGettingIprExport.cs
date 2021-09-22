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

    public class WhenGettingIprExport : ContextBase
    {
        private IResult<IEnumerable<IEnumerable<string>>> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new IPRSearchResource { FromDate = "01-Jan-2021", ToDate = "01-Jun-2021", IprResults = true};
            this.ReportService.GetIPRExport(1.January(2021), 1.June(2021), true).Returns(
                new ResultsModel { ReportTitle = new NameModel("IPR Import Books Report") });
            this.result = this.Sut.GetImpbookIprReportExport(resource);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ReportService.Received().GetIPRExport(1.January(2021), 1.June(2021), true);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<IEnumerable<string>>>>();
            var dataResult = ((SuccessResult<IEnumerable<IEnumerable<string>>>)this.result).Data;
        }
    }
}
