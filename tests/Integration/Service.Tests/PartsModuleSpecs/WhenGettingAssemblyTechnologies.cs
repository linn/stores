namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAssemblyTechnologies : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new AssemblyTechnology
                            {
                                Name = "A"
                            };
            var b = new AssemblyTechnology
                            {
                                Name = "B"
                            };

            this.AssemblyTechnologyService.GetAll()
                .Returns(new SuccessResult<IEnumerable<AssemblyTechnology>>(
                    new List<AssemblyTechnology> { a, b }));

            this.Response = this.Browser.Get(
                "/inventory/assembly-technologies",
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
            this.AssemblyTechnologyService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<AssemblyTechnologyResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Name == "A");
            resource.Should().Contain(a => a.Name == "B");
        }
    }
}