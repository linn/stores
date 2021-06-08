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

    public class WhenGettingById : ContextBase
    {
        private Consignment consignment;

        private int consignmentId;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 145;
            this.consignment = new Consignment { ConsignmentId = this.consignmentId };

            this.ConsignmentFacadeService.GetById(this.consignmentId)
                .Returns(new SuccessResult<Consignment>(this.consignment));

            this.Response = this.Browser.Get(
                $"/logistics/consignments/{this.consignmentId}",
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
            this.ConsignmentFacadeService.Received().GetById(this.consignmentId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<ConsignmentResource>();
            resultResource.ConsignmentId.Should().Be(this.consignmentId);
        }
    }
}
