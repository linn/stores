namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndCannotPutPartOnChosenPallet : ContextBase
    {
        private ProcessResult processResult;

        [SetUp]
        public void SetUp()
        {
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1235").Returns(true);
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1236").Returns(false);

            this.PalletAnalysisPack.Message().Returns("Cannot put a PART part on a PALLET pallet");
            this.processResult = this.Sut.DoBookIn(
                "O",
                1,
                "PART",
                null,
                1,
                1,
                1,
                null,
                null,
                null,
                null,
                null,
                "P1234",
                null,
                null,
                null,
                null,
                null,
                1,
                false,
                false,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry { StoragePlace = "P1234" },
                        new GoodsInLogEntry { StoragePlace = "P1235" },
                        new GoodsInLogEntry { StoragePlace = "P1236" }
                    });
        }

        [Test]
        public void ShouldNotCallProcedure()
        {
            this.GoodsInPack.DidNotReceive().DoBookIn(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<decimal>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                out Arg.Any<int?>(),
                out Arg.Any<bool>());
        }

        [Test]
        public void ShouldFailWithMessage()
        {
            this.processResult.Success.Should().BeFalse();
            this.processResult.Message.Should().Be("Can't put PART on P1236");
        }
    }
}
