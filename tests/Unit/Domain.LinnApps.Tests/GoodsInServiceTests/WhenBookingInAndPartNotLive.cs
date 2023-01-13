namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenBookingInAndPartNotLive : ContextBase
    {
        [Test]
        public void ShouldReturnErrorResult()
        {
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new Part { DateLive = null });
            this.StoragePlaceRepository.FindBy(Arg.Any<Expression<Func<StoragePlace, bool>>>())
                .Returns(new StoragePlace());
            this.StoresPack.ValidOrderQty(1, 1, 1, out Arg.Any<int>(), out Arg.Any<int>()).Returns(true);

            this.GoodsInPack.When(x => x.GetPurchaseOrderDetails(
                1,
                1,
                out var _,
                out var _,
                out var uom,
                out var _,
                out var _,
                out var _,
                out var _,
                out var _))
                .Do(x => x[4] = "ONES");
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);

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
                "PASS",
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
            result.Message.Should().Be("PART NOT LIVE - SEE PURCHASING!");
        }
    }
}
