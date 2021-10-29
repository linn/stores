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

    public class WhenGettingRsnConditions : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.RsnConditionsService.GetRsnConditions()
                .Returns(new SuccessResult<IEnumerable<RsnCondition>>(new List<RsnCondition>
                                                                          {
                                                                              new RsnCondition { Code = "AA" },
                                                                              new RsnCondition { Code = "AB" }
                                                                          }));

            this.Response = this.Browser.Get(
                "/logistics/goods-in/rsn-conditions",
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
            this.RsnConditionsService.Received().GetRsnConditions();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<RsnConditionResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Code == "AA");
            resource.Should().Contain(a => a.Code == "AB");
        }
    }
}
