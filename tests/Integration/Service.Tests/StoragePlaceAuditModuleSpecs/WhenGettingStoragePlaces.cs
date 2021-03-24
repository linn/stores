namespace Linn.Stores.Service.Tests.StoragePlaceAuditModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStoragePlaces : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var sp1 = new StoragePlace { Name = "sp1" };
            var sp2 = new StoragePlace { Name = "sp2" };

            this.StoragePlaceService.GetStoragePlaces("sp").Returns(
                new SuccessResult<IEnumerable<StoragePlace>>(new List<StoragePlace> { sp1, sp2 }));

            this.Response = this.Browser.Get(
                "/inventory/storage-places",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "sp");
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
            this.StoragePlaceService.Received().GetStoragePlaces("sp");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<StoragePlaceResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(s => s.Name == "sp1");
            resource.Should().Contain(s => s.Name == "sp2");
        }
    }
}
