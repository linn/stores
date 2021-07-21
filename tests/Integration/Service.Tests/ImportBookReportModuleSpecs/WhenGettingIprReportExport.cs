namespace Linn.Stores.Service.Tests.ImportBookReportModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingIprExport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new List<List<string>>();

            var resource = new IPRSearchResource { FromDate = "01-Jan-2021", ToDate = "01-Jun-2021" };

            this.ImportBookReportFacadeService.GetImpbookIprReportExport(Arg.Any<IPRSearchResource>()).Returns(
                new SuccessResult<IEnumerable<IEnumerable<string>>>(results));

            this.Response = this.Browser.Get(
                "/logistics/import-books/ipr/report/export",
                with =>
                    {
                        with.Header("Accept", "text/csv");
                        with.Query("fromDate", resource.FromDate);
                        with.Query("toDate", resource.ToDate);
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
            this.ImportBookReportFacadeService.Received().GetImpbookIprReportExport(Arg.Any<IPRSearchResource>());
        }
    }
}
