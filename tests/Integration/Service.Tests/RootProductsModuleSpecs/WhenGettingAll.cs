namespace Linn.Stores.Service.Tests.RootProductsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var rootProductA = new RootProduct
            {
                Name = "A",
                Description = "description A"
            };
            var rootProductB = new RootProduct
            {
                Name = "B",
                Description = "description B"
            };

            this.RootProductsService.GetValid()
                .Returns(new SuccessResult<IEnumerable<RootProduct>>(new List<RootProduct> { rootProductA, rootProductB }));


            this.Response = this.Browser.Get(
                "/inventory/root-products",
                with =>
                {
                    with.Header("Accept", "application/json");
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
            this.RootProductsService.Received().GetValid();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<RootProductResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Name == "A");
            resource.Should().Contain(a => a.Name == "B");
        }
    }
}