namespace Linn.Stores.Service.Tests.ImportBookReportModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingIprReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });

            var resource = new IPRSearchResource { FromDate = "01-Jan-2021", ToDate = "01-Jun-2021", IprResults = true };

            this.ImportBookReportFacadeService.GetImpbookIPRReport(Arg.Any<IPRSearchResource>()).Returns(
                new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel { ReportTitle = new NameModel("IPR Import Books Consignments") }
                    });

            this.Response = this.Browser.Get(
                "/logistics/import-books/ipr/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", resource.FromDate);
                        with.Query("toDate", resource.ToDate);
                        with.Query("iprResults", resource.IprResults.ToString());
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ImportBookReportFacadeService.Received().GetImpbookIPRReport(Arg.Any<IPRSearchResource>());
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("IPR Import Books Consignments");
        }
    }
}
