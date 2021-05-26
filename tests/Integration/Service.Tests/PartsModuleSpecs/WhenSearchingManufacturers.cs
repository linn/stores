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

    public class WhenSearchingManufacturers : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var manA = new Manufacturer
            {
               Code = "A",
               Description = "MAN A"
            };
            var manB = new Manufacturer
                           {
                               Code = "B",
                               Description = "MAN B"
                           };

            this.ManufacturerService.Search("MAN")
                .Returns(new SuccessResult<IEnumerable<Manufacturer>>(new List<Manufacturer> { manB, manA }));

            this.Response = this.Browser.Get(
                "/inventory/manufacturers",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", "MAN");
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
            this.ManufacturerService.Received().Search("MAN");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ManufacturerResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Description == "MAN A");
            resource.Should().Contain(a => a.Description == "MAN B");
        }
    }
}
