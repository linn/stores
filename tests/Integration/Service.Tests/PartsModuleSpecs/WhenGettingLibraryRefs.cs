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

    public class WhenGettingLibraryRefs : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new LibraryRef
                        {
                            LibraryName = "A",
                            Ref = "REF A"
                        };
            var b = new LibraryRef
                        {
                            LibraryName = "B",
                            Ref = "REF B"
                        };

            this.LibraryRefService.GetAll()
                .Returns(new SuccessResult<IEnumerable<LibraryRef>>(
                    new List<LibraryRef> { a, b }));

            this.Response = this.Browser.Get(
                "/parts/library-refs",
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
            this.LibraryRefService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<LibraryRefResource>>()
                .ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.LibraryName == "A");
            resource.Should().Contain(a => a.LibraryName == "B");
        }
    }
}
