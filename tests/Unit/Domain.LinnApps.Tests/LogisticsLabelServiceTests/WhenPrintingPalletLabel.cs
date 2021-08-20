namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingPalletLabel : ContextBase
    {
        private ProcessResult result;

        private int firstPallet;

        private int? lastPallet;

        [SetUp]
        public void SetUp()
        {
            this.firstPallet = 1;
            this.lastPallet = 1;

            this.result = this.Sut.PrintPalletLabel(
                this.ConsignmentId,
                this.firstPallet,
                this.lastPallet,
                this.UserNumber,
                1);
        }

        [Test]
        public void ShouldGetConsignment()
        {
            this.ConsignmentRepository.Received().FindById(this.ConsignmentId);
        }

        [Test]
        public void ShouldPrintLabel()
        {
            this.BartenderLabelPack.Received().PrintLabels(
                $"PalletLabel{this.ConsignmentId}pallet{this.firstPallet}",
                "DispatchLabels1",
                1,
                "dispatchpallet.btw",
                $"\"Big Shop{Environment.NewLine}this{Environment.NewLine}address{Environment.NewLine}d{Environment.NewLine}France\", \"1\", \"Pallet Number 1\", \"16\", \"808\"",
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be("1 pallet label(s) printed");
            this.result.Success.Should().BeTrue();
        }
    }
}
