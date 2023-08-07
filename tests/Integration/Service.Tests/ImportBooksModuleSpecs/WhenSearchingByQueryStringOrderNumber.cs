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

    public class WhenSearchingByQueryStringOrderNumber: ContextBase
    {
        private readonly int testOrderNumber = 05041990;

        [SetUp]

        public void SetUp()
        {
            var importBooks = new List<ImportBook> { new ImportBook { Id = 100, OrderDetails = new List<ImportBookOrderDetail> { new ImportBookOrderDetail { OrderNumber = this.testOrderNumber } } } };

            this.ImportBooksFacadeService
                .FilterBy(Arg.Is<ImportBookSearchResource>(x => x.SearchTerm.Equals($"&rsnNumber=&poNumber={this.testOrderNumber}&fromDate=&toDate=&customsEntryCodePrefix=&customsEntryCode=&customsEntryDate=")))
                .Returns(new SuccessResult<IEnumerable<ImportBook>>(importBooks));
            this.Response = this.Browser.Get(
                "/logistics/import-books",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", $"&rsnNumber=&poNumber={this.testOrderNumber}&fromDate=&toDate=&customsEntryCodePrefix=&customsEntryCode=&customsEntryDate=");
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
            this.ImportBooksFacadeService.Received().FilterBy(Arg.Is<ImportBookSearchResource>(x => x.SearchTerm.Equals($"&rsnNumber=&poNumber={this.testOrderNumber}&fromDate=&toDate=&customsEntryCodePrefix=&customsEntryCode=&customsEntryDate=")));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookResource>>();
            resource.Count().Should().Be(1);
            resource.FirstOrDefault(x => x.Id == 100).Should().NotBeNull();
            resource.FirstOrDefault(x => x.ImportBookOrderDetails.First().OrderNumber == this.testOrderNumber).Should().NotBeNull();
        }
    }
}
