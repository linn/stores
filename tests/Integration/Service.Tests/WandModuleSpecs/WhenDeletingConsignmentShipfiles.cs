namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingConsignmentShipfiles : ContextBase
    {
        private ConsignmentShipfile result;

        [SetUp]
        public void SetUp()
        {
            this.result = new ConsignmentShipfile
                              {
                                  ConsignmentId = 1,
                                  Consignment = new Consignment(),
                              };

            this.ShipfileService.DeleteShipfile(1)
                .Returns(new SuccessResult<ConsignmentShipfile>(this.result));

            this.Response = this.Browser.Delete(
                $"/logistics/shipfiles/1",
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
        public void ShouldCallFacadeService()
        {
            this.ShipfileService.Received().DeleteShipfile(1);
        }

        [Test]
        public void ShouldReturnDeleted()
        {
            var res = this.Response.Body.DeserializeJson<ConsignmentShipfile>();
            res.ConsignmentId.Should().Be(1);
        }
    }
}
