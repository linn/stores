namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartTqmsOverrides : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartTqmsOverride
                        {
                            Name = "A"
                        };
            var b = new PartTqmsOverride
                        {
                            Name = "B"
                        };

            this.TqmsCategoriesService.GetAll()
                .Returns(new SuccessResult<IEnumerable<PartTqmsOverride>>(
                    new List<PartTqmsOverride> { a, b }));

            this.Response = this.Browser.Get(
                "/parts/tqms-categories",
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
            this.TqmsCategoriesService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartTqmsOverrideResource>>()
                .ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Name == "A");
            resource.Should().Contain(a => a.Name == "B");
        }
    }
}
