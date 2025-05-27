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

    public class WhenGettingFootprintRefOptions : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new FootprintRefOption
                        {
                            LibraryName = "A",
                            Ref1 = "REF A"
                        };
            var b = new FootprintRefOption
            {
                            LibraryName = "B",
                            Ref2 = "REF B"
                        };

            this.FootprintRefOptionsService.GetOptions()
                .Returns(new SuccessResult<IEnumerable<FootprintRefOption>>(
                    new List<FootprintRefOption> { a, b }));

            this.Response = this.Browser.Get(
                "/parts/footprint-ref-options",
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
            this.FootprintRefOptionsService.Received().GetOptions();
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
