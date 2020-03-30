namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDespatchLocations : ContextBase
    {
        private DespatchLocation despatchLocation1;

        private DespatchLocation despatchLocation2;

        [SetUp]
        public void SetUp()
        {
            this.despatchLocation1 = new DespatchLocation { Id = 1, LocationCode = "one" };
            this.despatchLocation2 = new DespatchLocation { Id = 2, LocationCode = "two" };

            this.DespatchLocationFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<DespatchLocation>>(new List<DespatchLocation>
                                                                              {
                                                                                  this.despatchLocation1, this.despatchLocation2
                                                                              }));

            this.Response = this.Browser.Get(
                "/logistics/despatch-locations",
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
            this.DespatchLocationFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<DespatchLocationResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First(a => a.Id == 1).LocationCode.Should().Be("one");
            resultResource.First(a => a.Id == 2).LocationCode.Should().Be("two");
        }
    }
}
