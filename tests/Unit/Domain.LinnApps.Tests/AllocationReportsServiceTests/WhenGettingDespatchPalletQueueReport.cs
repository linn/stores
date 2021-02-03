namespace Linn.Stores.Domain.LinnApps.Tests.AllocationReportsServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDespatchPalletQueueReport : ContextBase
    {
        private DespatchPalletQueueResult result;

        [SetUp]
        public void SetUp()
        {
            var details = new List<DespatchPalletQueueDetail>
                              {
                                  new DespatchPalletQueueDetail
                                      {
                                          KittedFromTime = "29th Jan",
                                          PalletNumber = 1,
                                          PickingSequence = 32432432,
                                          WarehouseInformation = "at SA"
                                      },
                                  new DespatchPalletQueueDetail
                                      {
                                          KittedFromTime = "29th Jul",
                                          PalletNumber = 2,
                                          PickingSequence = 54154134,
                                          WarehouseInformation = "at SA"
                                      },
                                  new DespatchPalletQueueDetail
                                      {
                                          KittedFromTime = "29th Nov",
                                          PalletNumber = 455,
                                          PickingSequence = 63485623,
                                          WarehouseInformation = "waiting U24"
                                      }
                              };
            this.DespatchPalletQueueDetailRepository.FindAll()
                .Returns(details.AsQueryable());
            this.result = this.Sut.DespatchPalletQueue();
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.DespatchPalletQueueDetailRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.TotalNumberOfPallets.Should().Be(3);
            this.result.NumberOfPalletsToMove.Should().Be(2);
            this.result.DespatchPalletQueueResultDetails.Should().HaveCount(3);
            this.result.DespatchPalletQueueResultDetails.First(a => a.PalletNumber == 1).CanMoveToUpper.Should()
                .BeTrue();
            this.result.DespatchPalletQueueResultDetails.First(a => a.PalletNumber == 2).CanMoveToUpper.Should()
                .BeTrue();
            this.result.DespatchPalletQueueResultDetails.First(a => a.PalletNumber == 455).CanMoveToUpper.Should()
                .BeFalse();
        }
    }
}
