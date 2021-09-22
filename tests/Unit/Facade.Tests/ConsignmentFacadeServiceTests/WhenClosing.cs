namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosing : ContextBase
    {
        private ConsignmentUpdateResource updateResource;

        private IResult<Consignment> result;

        private int consignmentId;

        private int closedById;

        private int existingHubId;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 202;
            this.closedById = 123;
            this.existingHubId = 435;

            var consignment = new Consignment
                                  {
                                      ConsignmentId = this.consignmentId,
                                      HubId = this.existingHubId,
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
                                          ClosedById = 123,
                                          HubId = 2222,
                                          Status = "C"
                                      };
                                          
            this.ConsignmentRepository.FindById(this.consignmentId).Returns(consignment);

            this.result = this.Sut.Update(this.consignmentId, this.updateResource);
        }

        [Test]
        public void ShouldCallCloseService()
        {
            this.ConsignmentService.Received().CloseConsignment(
                Arg.Is<Consignment>(c => c.ConsignmentId == this.consignmentId),
                this.closedById);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<Consignment>>();
            var updatedConsignment = ((SuccessResult<Consignment>)this.result).Data;
            updatedConsignment.HubId.Should().Be(this.existingHubId);
            updatedConsignment.Status.Should().Be("C");
            updatedConsignment.ClosedById.Should().Be(this.closedById);
            updatedConsignment.DateClosed.Should().HaveValue();
        }
    }
}
