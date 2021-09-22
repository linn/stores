namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Consignments.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPackingList : ContextBase
    {
        private PackingList result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetPackingList(this.ConsignmentId);
        }

        [Test]
        public void GetConsignment()
        {
            this.ConsignmentRepository.Received().FindById(this.ConsignmentId);
        }

        [Test]
        public void ShouldReturnConsignmentDetails()
        {
            this.result.ConsignmentId.Should().Be(this.ConsignmentId);
            this.result.DeliveryAddress.Id.Should().Be(this.Consignment.Address.Id);
            this.result.DespatchDate.Should().Be(this.Consignment.DateClosed);
            this.result.SenderAddress.Addressee.Should().Be("Linn Products Ltd");
            this.result.NumberOfPallets.Should().Be(1);
            this.result.NumberOfItemsNotOnPallets.Should().Be(3);
            this.result.TotalGrossWeight.Should().Be(19.5m);
            this.result.TotalVolume.Should().Be(1.94m);
        }

        [Test]
        public void ShouldReturnThreeLooseItems()
        {
            this.result.Items.Should().HaveCount(3);
            var firstItem = this.result.Items.First(a => a.ItemNumber == 1);
            firstItem.Description.Should().Be("1 Single Thing");
            firstItem.Volume.Should().Be(0.125m);
            firstItem.Weight.Should().Be(2);
            firstItem.DisplayDimensions.Should().Be("50 x 50 x 50cm");
            firstItem.Description.Should().Be("1 Single Thing");
            this.result.Items.First(a => a.ItemNumber == 1).Description.Should().Be("1 Single Thing");
            this.result.Items.First(a => a.ItemNumber == 2).Description.Should().Be("1 Boxed Thing");
            this.result.Items.First(a => a.ItemNumber == 4).Description.Should().Be("1 Sealed Thing 1");
        }

        [Test]
        public void ShouldReturnOnePallet()
        {
            this.result.Pallets.Should().HaveCount(1);
            
            var pallet = this.result.Pallets.First();
            pallet.PalletNumber.Should().Be(1);
            pallet.DisplayDimensions.Should().Be("120 x 120 x 100cm");
            pallet.DisplayWeight.Should().Be("14 Kgs");
            pallet.Volume.Should().Be(1.44m);
            pallet.Items.Should().HaveCount(1);
            var palletItem = pallet.Items.First();
            palletItem.ItemNumber.Should().Be(5);
            palletItem.Description.Should().Be("2 Sealed Thing 2");
            palletItem.Weight.Should().Be(2);
            palletItem.DisplayDimensions.Should().Be("150 x 150 x 150cm");
            palletItem.Volume.Should().Be(3.375m);
        }
    }
}
