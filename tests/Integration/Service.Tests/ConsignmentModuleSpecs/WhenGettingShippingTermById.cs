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

    public class WhenGettingShippingTermById : ContextBase
    {
        private ShippingTerm shippingTerm;

        private int shippingTermId;

        [SetUp]
        public void SetUp()
        {
            this.shippingTermId = 145;
            this.shippingTerm = new ShippingTerm { Id = this.shippingTermId };

            this.ShippingTermFacadeService.GetById(this.shippingTermId)
                .Returns(new SuccessResult<ShippingTerm>(this.shippingTerm));

            this.Response = this.Browser.Get(
                $"/logistics/shipping-terms/{this.shippingTermId}",
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
            this.ShippingTermFacadeService.Received().GetById(this.shippingTermId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<ShippingTermResource>();
            resultResource.Id.Should().Be(this.shippingTermId);
        }
    }
}
