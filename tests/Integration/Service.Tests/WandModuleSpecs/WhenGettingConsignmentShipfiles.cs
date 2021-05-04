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

    public class WhenGettingConsignmentShipfiles : ContextBase
    {
        private IEnumerable<ConsignmentShipfile> shipfiles;

        [SetUp]
        public void SetUp()
        {
            this.shipfiles = new List<ConsignmentShipfile>
                                 {
                                     new ConsignmentShipfile
                                         {
                                             ConsignmentId = 1, Consignment = new Consignment
                                                                                  {
                                                                                      ConsignmentId = 1,
                                                                                      Invoices = new List<Invoice>
                                                                                          {
                                                                                              new Invoice
                                                                                                  {
                                                                                                      ConsignmentId = 1
                                                                                                  }
                                                                                          }
                                                                                  }
                                         },
                                 };
            this.ShipfileService.GetShipfiles()
                .Returns(new SuccessResult<IEnumerable<ConsignmentShipfile>>(this.shipfiles));

            this.Response = this.Browser.Get(
                "/logistics/shipfiles",
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
            this.ShipfileService.Received().GetShipfiles();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResources = this.Response
                .Body.DeserializeJson<IEnumerable<ConsignmentShipfileResource>>().ToList();
            resultResources.Should().HaveCount(1);
            resultResources.Should().Contain(a => a.ConsignmentId == 1);
        }
    }
}
