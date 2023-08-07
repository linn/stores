namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingShipfile : ContextBase
    {
        private ConsignmentShipfile result;

        private ConsignmentShipfileResource resource;

        [SetUp]
        public void SetUp()
        {
            this.result = new ConsignmentShipfile
                              {
                                  ConsignmentId = 1,
                                  Consignment = new Consignment(),
                              };


            this.resource = new ConsignmentShipfileResource { InvoiceNumbers = "123" };

            this.ShipfileService.Add(Arg.Any<ConsignmentShipfileResource>())
                .Returns(new CreatedResult<ConsignmentShipfile>(this.result));

            this.Response = this.Browser.Post(
                $"/logistics/shipfiles/",
                with =>
                    {
                        with.JsonBody(this.resource);
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallFacadeService()
        {
            this.ShipfileService.Received().Add(Arg.Any<ConsignmentShipfileResource>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            var res = this.Response.Body.DeserializeJson<ConsignmentShipfile>();
            res.ConsignmentId.Should().Be(1);
        }
    }
}
