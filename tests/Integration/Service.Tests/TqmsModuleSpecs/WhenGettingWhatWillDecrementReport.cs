namespace Linn.Stores.Service.Tests.TqmsModuleSpecs
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

    public class WhenGettingTqmsSummaryByCategory : ContextBase
    {
        private string jobRef;

        [SetUp]
        public void SetUp()
        {
            this.jobRef = "ABC";
            var results = new ResultsModel(new[] { "col1" });

            this.TqmsReportsFacadeService.GetTqmsSummaryByCategory(this.jobRef)
                .Returns(new SuccessResult<ResultsModel>(results)
                             {
                                 Data = new ResultsModel { ReportTitle = new NameModel("title") }
                             });

            this.Response = this.Browser.Get(
                "/inventory/tqms-category-summary/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("JobRef", this.jobRef);
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
            this.TqmsReportsFacadeService.Received().GetTqmsSummaryByCategory(this.jobRef);
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
