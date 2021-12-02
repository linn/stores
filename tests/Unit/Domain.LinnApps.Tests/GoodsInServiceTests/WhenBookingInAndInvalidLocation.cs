namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenBookingInAndInvalidLocation : ContextBase
    {
        [Test]
        public void ShouldReturnErrorResult()
        {
            this.StoragePlaceRepository.FindBy(Arg.Any<Expression<Func<StoragePlace, bool>>>()).ReturnsNull();
            var result = this.Sut.DoBookIn(
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
                "QC",
                null,
                null,
                null,
                null,
                1,
                false,
                false,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                OrderLine = 1,
                                OrderNumber = 1,
                                Quantity = 1,
                                StoragePlace = "P1234"
                            }
                    });

            result.Success.Should().BeFalse();
            result.Message.Should().Be("Invalid location entered");
        }
    }
}
