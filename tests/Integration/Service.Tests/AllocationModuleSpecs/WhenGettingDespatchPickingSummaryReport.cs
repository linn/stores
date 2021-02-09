namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
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

    public class WhenGettingDespatchPickingSummaryReport : ContextBase
    {
        private ResultsModel results;

        [SetUp]
        public void SetUp()
        {
            this.results = new ResultsModel { ReportTitle = new NameModel("report") };
            this.AllocationFacadeService.DespatchPickingSummaryReport()
                .Returns(new SuccessResult<ResultsModel>(this.results));

            this.Response = this.Browser.Get(
                "/logistics/allocations/despatch-picking-summary",
                with =>
                {
                    with.Header("Accept", "application/json");
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
            this.AllocationFacadeService.Received().DespatchPickingSummaryReport();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resultResource.ReportResults.First().title.displayString.Should().Be(this.results.ReportTitle.DisplayValue);
        }
    }
}
