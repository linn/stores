namespace Linn.Stores.Service.Tests.QcPartsReportModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingExport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Get(
                "/inventory/reports/qc-parts/export",
                with =>
                    {
                        with.Header("Accept", "text/csv");
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
            this.MockDomainService.Received().GetReport();
        }
    }
}
