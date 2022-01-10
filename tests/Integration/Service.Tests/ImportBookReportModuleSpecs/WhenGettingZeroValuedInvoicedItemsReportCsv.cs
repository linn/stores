namespace Linn.Stores.Service.Tests.ImportBookReportModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Resources.ImportBooks;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingZeroValuedInvoicedItemsReportCsv : ContextBase
    {
        private FromToDateResource resource;

        [SetUp]
        public void SetUp()
        {
            var results = new List<List<string>>();

            this.resource = new FromToDateResource { From = "01-Jan-2021", To = "01-Jun-2021" };

            this.ZeroValuedInvoiceDetailsReportService
                .GetCsvReport(this.resource.From, this.resource.To)
                .Returns(
                new SuccessResult<IEnumerable<IEnumerable<string>>>(results));

            this.Response = this.Browser.Get(
                "logistics/zero-valued-invoiced-report",
                with =>
                    {
                        with.Header("Accept", "text/csv");
                        with.Query("from", this.resource.From);
                        with.Query("to", this.resource.To);
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
            this.ZeroValuedInvoiceDetailsReportService.Received()
                .GetCsvReport(this.resource.From, this.resource.To);
        }
    }
}
