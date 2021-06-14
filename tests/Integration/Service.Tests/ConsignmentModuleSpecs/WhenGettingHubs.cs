namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingHubs : ContextBase
    {
        private Hub hub1;

        private Hub hub2;

        [SetUp]
        public void SetUp()
        {
            this.hub1 = new Hub { HubId = 1 };
            this.hub2 = new Hub { HubId = 2 };

            this.HubFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Hub>>(new List<Hub>
                                                                 {
                                                                     this.hub1, this.hub2
                                                                 }));

            this.Response = this.Browser.Get(
                "/logistics/hubs",
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
            this.HubFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<HubResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.Should().Contain(a => a.HubId == 1);
            resultResource.Should().Contain(a => a.HubId == 2);
        }
    }
}
