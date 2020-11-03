namespace Linn.Stores.Service.Tests.WhatWillDecrementReportModuleSpecs
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

    public class WhenGettingWhatWillDecrementReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });

            this.WhatWillDecrementReportFacadeService.GetWhatWillDecrementReport("PART", 1, "TYPE", "CODE").Returns(
                new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel { ReportTitle = new NameModel("title") }
                    });

            this.Response = this.Browser.Get(
                "/inventory/reports/what-will-decrement/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("partNumber", "PART");
                        with.Query("quantity", "1");
                        with.Query("typeOfRun", "TYPE");
                        with.Query("workstationCode", "CODE");
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
            this.WhatWillDecrementReportFacadeService.Received().GetWhatWillDecrementReport("PART", 1, "TYPE", "CODE");
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}