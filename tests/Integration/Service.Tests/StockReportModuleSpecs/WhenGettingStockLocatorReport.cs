namespace Linn.Stores.Service.Tests.StockReportModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStockLocatorReport : ContextBase
    {
        private string siteCode;

        [SetUp]
        public void SetUp()
        {
            this.siteCode = "S";
            var results = new ResultsModel { ReportTitle = new NameModel("Title") };

            this.StockReportService.GetStockLocatorReport(this.siteCode)
                .Returns(results);

            this.Response = this.Browser.Get(
                "/inventory/reports/stock-locators-report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("siteCode", this.siteCode);
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
            this.StockReportService.Received().GetStockLocatorReport(this.siteCode);
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("Title");
        }
    }
}
