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

    public class WhenGettingShippingTerms : ContextBase
    {
        private ShippingTerm shippingTerm1;

        private ShippingTerm shippingTerm2;

        [SetUp]
        public void SetUp()
        {
            this.shippingTerm1 = new ShippingTerm { Id = 1 };
            this.shippingTerm2 = new ShippingTerm { Id = 2 };

            this.ShippingTermFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<ShippingTerm>>(
                    new List<ShippingTerm> { this.shippingTerm1, this.shippingTerm2 }));

            this.Response = this.Browser.Get(
                "/logistics/shipping-terms",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ShippingTermFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<ShippingTermResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.Should().Contain(a => a.Id == 1);
            resultResource.Should().Contain(a => a.Id == 2);
        }
    }
}
