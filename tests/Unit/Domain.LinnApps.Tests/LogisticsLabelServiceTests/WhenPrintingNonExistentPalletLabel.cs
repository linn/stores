namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NUnit.Framework;

    public class WhenPrintingNonExistentPalletLabel : ContextBase
    {
        private int firstPallet;

        private int? lastPallet;

        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.firstPallet = 156;
            this.lastPallet = 156;

            this.result = this.Sut.PrintPalletLabel(
                this.ConsignmentId,
                this.firstPallet,
                this.lastPallet,
                this.UserNumber,
                1);
        }

        [Test]
        public void ShouldReturnFailedResult()
        {
            this.result.Message.Should().Be("Printing Failed. Could not find pallet 156 on consignment 808");
            this.result.Success.Should().BeFalse();
        }
    }
}
