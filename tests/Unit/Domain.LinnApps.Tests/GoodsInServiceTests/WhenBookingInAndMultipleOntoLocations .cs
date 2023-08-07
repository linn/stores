namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndMultipleOntoLocations : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PurchaseOrderPack.GetDocumentType(1).Returns("PO");
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", Arg.Any<string>()).Returns(true);
            this.GoodsInPack.GetNextBookInRef().ReturnsForAnyArgs(1);
            this.StoresPack.ValidOrderQty(1, 1, 3, out Arg.Any<int>(), out Arg.Any<int>()).Returns(true);

            this.ReqRepository.FindById(1).Returns(
                new RequisitionHeader
                    {
                        ReqNumber = 1,
                        Lines = new List<RequisitionLine>
                                    {
                                        new RequisitionLine
                                            {
                                                TransactionDefinition =
                                                    new StoresTransactionDefinition { DocType = "PO" }
                                            }
                                    }
                    });
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(
                new Part
                    {
                        PartNumber = "PART", 
                        Description = "A PART", 
                        QcInformation = "Some Info",
                        DateLive = DateTime.Today
                });

            this.GoodsInPack
                .When(
                    x => x.GetPurchaseOrderDetails(
                        1,
                        1,
                        out var _,
                        out var _,
                        out var uom,
                        out var _,
                        out var _,
                        out var _,
                        out var _,
                        out var _)).Do(x => x[4] = "ONES");

            this.GoodsInPack
                .When(
                    x => x.DoBookIn(
                        1,
                        "O",
                        1,
                        "PART",
                        3,
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
                        out var success)).Do(x => { x[18] = true; });

            this.GoodsInPack.GetNextLogId().Returns(1111);

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
                            },
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                OrderLine = 1,
                                OrderNumber = 1,
                                Quantity = 1,
                                StoragePlace = "P12345"
                            },
                        new GoodsInLogEntry
                            {
                                 ArticleNumber = "PART",
                                 DateCreated = DateTime.UnixEpoch,
                                 OrderLine = 1,
                                 OrderNumber = 1,
                                 Quantity = 1,
                                 StoragePlace = "P123456"
                            }
                    });
        }

        [Test]
        public void ShouldCallStoredProcedureWithTotalQty()
        {
            this.GoodsInPack.Received().DoBookIn(
                1,
                "O",
                1,
                "PART",
                3, // total qty of lines = 3
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
        public void ShouldReturnSuccessResult()
        {
            this.result.Success.Should().BeTrue();
            this.result.DocType.Should().Be("PO");
            this.result.PartNumber.Should().Be("PART");
        }
    }
}
