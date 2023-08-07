namespace Linn.Stores.Service.Tests.ImportBooksReportModuleTests
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private FromToDateResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new FromToDateResource
                           {
                               From = new DateTime(2022, 06, 1).ToString("o"),
                               To = new DateTime(2022, 08, 31).ToString("o")
                           };

            this.Response = this.Browser.Get(
                "/logistics/reports/eu-credit-invoices/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("from", this.resource.From);
                        with.Query("to", this.resource.To);
                    }).Result;
        }

        [Test]
        public void ShouldReturnReport()
        {
            var res = this.Response.Body.DeserializeJson<ReportReturnResource>();
            res.ReportResults.First().title.displayString.Should().Be("EU Credit Invoices");
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
