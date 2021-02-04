namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStorageLocations : ContextBase
    {
        private StorageLocation l1;

        private StorageLocation l2;

        [SetUp]
        public void SetUp()
        {
            this.l1 = new StorageLocation { LocationCode = "A", };
            this.l2 = new StorageLocation { LocationCode = "AA" };

            this.StorageLocationService.Search(Arg.Any<string>())
                .Returns(new
                    SuccessResult<IEnumerable<StorageLocation>>(new List<StorageLocation>
                                                                 {
                                                                     this.l1, this.l2
                                                                 }));

            this.Response = this.Browser.Get(
                "/inventory/storage-locations",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "A");
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
            this.StorageLocationService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StorageLocation>>().ToList();
            resultResource.Should().HaveCount(2);
        }
    }
}
