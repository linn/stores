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

    public class WhenGettingPartLibraries : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartLibrary
                        {
                            LibraryName = "A"
                        };
            var b = new PartLibrary 
                        {
                            LibraryName = "B"
                        };

            this.PartLibrariesService.GetAll()
                .Returns(new SuccessResult<IEnumerable<PartLibrary>>(
                    new List<PartLibrary> { a, b }));

            this.Response = this.Browser.Get(
                "/parts/libraries",
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
            this.PartLibrariesService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartLibraryResource>>()
                .ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.LibraryName == "A");
            resource.Should().Contain(a => a.LibraryName == "B");
        }
    }
}
