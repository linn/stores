namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingShipfileEmails : ContextBase
    {
        private ConsignmentShipfilesSendEmailsRequestResource resource;

        private IEnumerable<ConsignmentShipfile> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ConsignmentShipfilesSendEmailsRequestResource
                                {
                                    Test = false,
                                    Shipfiles = new List<ConsignmentShipfileResource>
                                                    {
                                                        new ConsignmentShipfileResource
                                                            {
                                                                ConsignmentId = 1,
                                                            }
                                                    }
                                };

            this.result = new List<ConsignmentShipfile>
                              {
                                  new ConsignmentShipfile
                                      {
                                          ConsignmentId = 1,
                                          Consignment = new Consignment()
                                      }
                              };

            this.ShipfileService.SendEmails(Arg.Any<ConsignmentShipfilesSendEmailsRequestResource>())
                .Returns(new SuccessResult<IEnumerable<ConsignmentShipfile>>(this.result));

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
            this.ShipfileService.Received().SendEmails(Arg.Any<ConsignmentShipfilesSendEmailsRequestResource>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            var res = this.Response.Body.DeserializeJson<IEnumerable<ConsignmentShipfile>>();
            res.First().ConsignmentId.Should().Be(1);
        }
    }
}

