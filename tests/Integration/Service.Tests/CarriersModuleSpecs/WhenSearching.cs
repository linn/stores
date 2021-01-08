namespace Linn.Stores.Service.Tests.CarriersModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var carrierA = new Carrier
            {
                CarrierCode = "code numma 1",
                Name = "Carrier A",
                OrganisationId = 112
            };

            var carrierB = new Carrier
            {
                CarrierCode = "code numma 2",
                Name = "Carrier B",
                OrganisationId = 118
            };

            this.CarriersService.SearchCarriers(Arg.Any<string>())
                .Returns(new SuccessResult<IEnumerable<Carrier>>(new List<Carrier> { carrierA, carrierB }));

            var searchRequestResource = new SearchRequestResource { SearchTerm = "code numma" };

            this.Response = this.Browser.Get(
                "/logistics/carriers",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(searchRequestResource);
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
            this.CarriersService.Received().SearchCarriers(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<CarrierResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Name == "Carrier A");
            resource.Should().Contain(a => a.Name == "Carrier B");
        }
    }
}
