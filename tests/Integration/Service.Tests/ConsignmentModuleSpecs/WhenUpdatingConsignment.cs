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

    public class WhenUpdatingConsignment : ContextBase
    {
        private ConsignmentUpdateResource requestResource;

        private int consignmentId;

        private string newCarrier;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 4358;
            this.newCarrier = "Careful";
            this.requestResource = new ConsignmentUpdateResource { Carrier = this.newCarrier };
            var consignment = new Consignment { ConsignmentId = this.consignmentId };
            this.ConsignmentFacadeService.Update(this.consignmentId, Arg.Any<ConsignmentUpdateResource>())
                .Returns(new SuccessResult<Consignment>(consignment));

            this.Response = this.Browser.Put(
                $"logistics/consignments/{this.consignmentId}",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.ConsignmentFacadeService
                .Received()
                .Update(this.consignmentId, Arg.Is<ConsignmentUpdateResource>(r => r.Carrier == this.newCarrier));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ConsignmentResource>();
            resource.ConsignmentId.Should().Be(this.consignmentId);
        }
    }
}
