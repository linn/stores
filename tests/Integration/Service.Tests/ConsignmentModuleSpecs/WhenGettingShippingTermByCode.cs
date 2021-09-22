namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingShippingTermByCode : ContextBase
    {
        private ShippingTerm shippingTerm;

        private string code;

        [SetUp]
        public void SetUp()
        {
            this.code = "Q.D";
            this.shippingTerm = new ShippingTerm { Id = 1, Code = this.code };

            this.ShippingTermFacadeService.Search(this.code)
                .Returns(new SuccessResult<IEnumerable<ShippingTerm>>(new List<ShippingTerm> { this.shippingTerm }));

            this.Response = this.Browser.Get(
                $"/logistics/shipping-terms/",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.code);
                    with.Query("exactOnly", "true");
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
            this.ShippingTermFacadeService.Received().Search(this.code);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<ShippingTermResource>();
            resultResource.Code.Should().Be(this.code);
        }
    }
}
