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

    public class WhenBookingInQcPart : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PurchaseOrderPack.GetDocumentType(1).Returns("PO");
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.GoodsInPack.GetNextBookInRef().ReturnsForAnyArgs(1);
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

            this.GoodsInPack.When(x => x.DoBookIn(
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
                "QC",
                null,
                null,
                null,
                out var reqNumber,
                out var success))
                .Do(x =>
                {
                    x[17] = 1;
                    x[18] = true;
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
                "QC",
                null,
                null,
                null,
                null,
                1,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                OrderLine = 1,
                                OrderNumber = 1
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
                "QC",
                null,
                null,
                null,
                out var reqNumber,
                out var success);
        }

        [Test]
        public void ShouldSetQcState()
        {
            this.result.QcState.Should().Be("QUARANTINE");
        } 
    }
}
