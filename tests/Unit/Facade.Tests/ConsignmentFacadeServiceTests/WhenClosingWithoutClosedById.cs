namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingWithoutClosedById : ContextBase
    {
        private ConsignmentUpdateResource updateResource;

        private IResult<Consignment> result;

        private int consignmentId;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 202;

            var consignment = new Consignment
                                  {
                                      ConsignmentId = this.consignmentId,
                                      HubId = 1,
                                      Carrier = "Clumsy",
                                      Terms = "R2D2",
                                      Status = "L",
                                      ShippingMethod = "Throw",
                                      DespatchLocationCode = "MoonBase Alpha",
                                      Pallets = new List<ConsignmentPallet>
                                                    {
                                                        new ConsignmentPallet
                                                            {
                                                                PalletNumber = 1, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                            },
                                                        new ConsignmentPallet
                                                            {
                                                                PalletNumber = 12, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                            },
                                                    },
                                      Items = new List<ConsignmentItem>
                                                  {
                                                      new ConsignmentItem
                                                          {
                                                              ItemNumber = 1, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                          },
                                                      new ConsignmentItem
                                                          {
                                                              ItemNumber = 12, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                          },
                                                  }
                                  };

            this.updateResource = new ConsignmentUpdateResource
                                      {
                                          HubId = 2222,
                                          Status = "C"
                                      };
                                          
            this.ConsignmentRepository.FindById(this.consignmentId).Returns(consignment);

            this.result = this.Sut.Update(this.consignmentId, this.updateResource);
        }

        [Test]
        public void ShouldNotCallCloseService()
        {
            this.ConsignmentService.DidNotReceive().CloseConsignment(
                Arg.Any<Consignment>(),
                Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<BadRequestResult<Consignment>>();
            var badRequestResult = (BadRequestResult<Consignment>)this.result;
            badRequestResult.Message.Should().Be("Error updating 202 - Closed by id must be provided to close consignment");
        }
    }
}
