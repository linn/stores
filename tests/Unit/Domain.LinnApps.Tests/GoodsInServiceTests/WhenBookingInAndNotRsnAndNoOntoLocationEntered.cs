namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndNotRsnAndNoOntoLocationEntered : ContextBase
    {
        private ProcessResult processResult;

        [SetUp]
        public void SetUp()
        {
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new Part { DateLive = DateTime.Today });
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
                null,
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
        public void ShouldFailWithMessage()
        {
            this.processResult.Success.Should().BeFalse();
            this.processResult.Message.Should().Be("Onto location/pallet must be entered");
        }
    }
}
