namespace Linn.Stores.Service.Tests.CountryModuleSpecs
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
        private Country country1;

        private Country country2;

        [SetUp]
        public void SetUp()
        {
            this.country1 = new Country { CountryCode = "a", Name = "one" };
            this.country2 = new Country { CountryCode = "b", Name = "two" };

            this.CountryFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Country>>(new List<Country>
                                                                              {
                                                                                  this.country1, this.country2
                                                                              }));

            this.Response = this.Browser.Get(
                "/logistics/countries",
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
            this.CountryFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<CountryResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First(a => a.CountryCode == "a").Name.Should().Be("one");
            resultResource.First(a => a.CountryCode == "b").Name.Should().Be("two");
        }
    }
}
