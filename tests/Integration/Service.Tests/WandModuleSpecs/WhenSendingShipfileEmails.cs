namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingShipfileEmails : ContextBase
    {
        private ConsignmentShipfileSendEmailsRequestResource resource;

        private ConsignmentShipfile result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ConsignmentShipfileSendEmailsRequestResource
                                {
                                    Test = false,
                                    Shipfile = new ConsignmentShipfileResource
                                                            {
                                                                ConsignmentId = 1,
                                                            }
                                };

            this.result = new ConsignmentShipfile
                                      {
                                          ConsignmentId = 1,
                                          Consignment = new Consignment()
                                      };

            this.ShipfileService.SendEmails(Arg.Any<ConsignmentShipfileSendEmailsRequestResource>())
                .Returns(new SuccessResult<ConsignmentShipfile>(this.result));

            this.Response = this.Browser.Post(
                $"/logistics/shipfiles/send-emails",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(this.resource);
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
            this.ShipfileService.Received().SendEmails(Arg.Any<ConsignmentShipfileSendEmailsRequestResource>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            var res = this.Response.Body.DeserializeJson<ConsignmentShipfile>();
            res.ConsignmentId.Should().Be(1);
        }
    }
}
