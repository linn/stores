namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;
    using NSubstitute;

    using NUnit.Framework;
    using System.Linq.Expressions;
    using System;

    public class WhenBookingInAndNoBookInLines : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new Part { DateLive = DateTime.Today });
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.result = this.Sut.DoBookIn(
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
                new GoodsInLogEntry[0]);
        }

        [Test]
        public void ShouldReturnFailState()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("Nothing to book in");
        }
    }
}
