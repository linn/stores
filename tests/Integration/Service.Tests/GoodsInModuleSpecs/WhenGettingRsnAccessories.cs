namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingRsnAccessories : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.RsnAccessoriesService.GetRsnAccessories()
                .Returns(new SuccessResult<IEnumerable<RsnAccessory>>(new List<RsnAccessory>
                                                                          {
                                                                              new RsnAccessory { Code = "AA" },
                                                                              new RsnAccessory { Code = "AB" }
                                                                          }));

            this.Response = this.Browser.Get(
                "/logistics/goods-in/rsn-accessories",
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
            this.RsnAccessoriesService.Received().GetRsnAccessories();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<RsnAccessoryResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Code == "AA");
            resource.Should().Contain(a => a.Code == "AB");
        }
    }
}
