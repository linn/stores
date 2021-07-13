namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingCpcNumbers : ContextBase
    {
        private readonly ImportBookCpcNumber firstCpc =
            new ImportBookCpcNumber { CpcNumber = 1, Description = "cpc something" };

        private readonly ImportBookCpcNumber secondCpc =
            new ImportBookCpcNumber { CpcNumber = 2, Description = "another thing" };

        [SetUp]
        public void SetUp()
        {
            var cpcNumbers = new List<ImportBookCpcNumber> { this.firstCpc, this.secondCpc };

            this.ImportBookCpcNumberFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<ImportBookCpcNumber>>(cpcNumbers));

            this.Response = this.Browser.Get(
                "/logistics/import-books/cpc-numbers",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ImportBookCpcNumberFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookCpcNumberResource>>();
            resource.Count().Should().Be(2);
            resource.FirstOrDefault(x => x.CpcNumber == this.firstCpc.CpcNumber && x.Description == this.firstCpc.Description).Should().NotBeNull();
            resource.FirstOrDefault(x => x.CpcNumber == this.secondCpc.CpcNumber && x.Description == this.secondCpc.Description).Should().NotBeNull();
        }
    }
}
