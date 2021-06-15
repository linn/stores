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

    public class WhenGettingCarriers : ContextBase
    {
        private Carrier carrier1;

        private Carrier carrier2;

        [SetUp]
        public void SetUp()
        {
            this.carrier1 = new Carrier { CarrierCode = "C1" };
            this.carrier2 = new Carrier { CarrierCode = "C2" };

            this.CarrierFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Carrier>>(new List<Carrier>
                                                                     {
                                                                         this.carrier1, this.carrier2
                                                                     }));

            this.Response = this.Browser.Get(
                "/logistics/carriers",
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
            this.CarrierFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<CarrierResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.Should().Contain(a => a.CarrierCode == "C1");
            resultResource.Should().Contain(a => a.CarrierCode == "C2");
        }
    }
}
