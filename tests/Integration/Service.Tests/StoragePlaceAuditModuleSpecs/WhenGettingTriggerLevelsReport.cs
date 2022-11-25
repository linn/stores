namespace Linn.Stores.Service.Tests.StoragePlaceAuditModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTriggerLevelsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });

            this.TriggerLevelReportService.GetReport("loc").Returns(
                new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel { ReportTitle = new NameModel("title") }
                    });

            this.Response = this.Browser.Get(
                "/inventory/storage-places/reports/stock-trigger-levels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "loc");
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
            this.TriggerLevelReportService.Received().GetReport("loc");
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
