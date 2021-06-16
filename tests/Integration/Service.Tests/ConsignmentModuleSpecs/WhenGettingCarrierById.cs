namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingCarrierById : ContextBase
    {
        private Carrier carrier;

        private string carrierCode;

        [SetUp]
        public void SetUp()
        {
            this.carrierCode = "Fred";
            this.carrier = new Carrier { CarrierCode = this.carrierCode };

            this.CarrierFacadeService.GetById(this.carrierCode)
                .Returns(new SuccessResult<Carrier>(this.carrier));

            this.Response = this.Browser.Get(
                $"/logistics/carriers/{this.carrierCode}",
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
            this.CarrierFacadeService.Received().GetById(this.carrierCode);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<CarrierResource>();
            resultResource.CarrierCode.Should().Be(this.carrierCode);
        }
    }
}
