namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndStoredProcedureCallFails : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PurchaseOrderPack.GetDocumentType(1).Returns("PO");
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.GoodsInPack.GetNextBookInRef().ReturnsForAnyArgs(1);
            this.StoresPack.ValidOrderQty(1, 1, 1, out Arg.Any<int>(), out Arg.Any<int>()).Returns(true);
            this.ReqRepository.FindById(1).Returns(new RequisitionHeader { ReqNumber = 1, });
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new Part
                {
                    PartNumber = "PART",
                    Description = "A PART",
                    QcInformation = "Some Info"
                });

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

            this.GoodsInPack.DoBookIn(
                1,
                "O",
                1,
                "PART",
                1,
                1,
                1,
                null,
                null,
                null,
                "P1234",
                null,
                null,
                null,
                null,
                null,
                null,
                out var reqNumber,
                out var success)
                .Returns(x =>
                {
                    x[17] = null;
                    x[18] = false;
                    return "Something went wrong...";
                });

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
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                OrderLine = 1,
                                OrderNumber = 1,
                                Quantity = 1
                            }
                    });
        }

        [Test]
        public void ShouldCallStoredProcedure()
        {
            this.GoodsInPack.Received().DoBookIn(
                1,
                "O",
                1,
                "PART",
                1,
                1,
                1,
                null,
                null,
                null,
                "P1234",
                null,
                null,
                null,
                null,
                null,
                null,
                out var reqNumber,
                out var success);
        }

        [Test]
        public void ShouldReturnFailResult()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("Something went wrong...");
            this.result.Lines.Should().BeNullOrEmpty();
        }
    }
}
