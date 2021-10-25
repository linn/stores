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

    public class WhenBookingInLoanReturn : ContextBase
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
                "L",
                1,
                "PART",
                1,
                null,
                null,
                1,
                1,
                null,
                "P1234",
                null,
                null,
                null,
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

            this.GoodsInPack.GetNextLogId().Returns(1111);

            this.result = this.Sut.DoBookIn(
                "L",
                1,
                "PART",
                null,
                1,
                null,
                null,
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
                1,
                false,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                LoanLine = 1,
                                LoanNumber = 1
                            }
                    });
        }

        [Test]
        public void ShouldCallStoredProcedure()
        {
            this.GoodsInPack.Received().DoBookIn(
                1,
                "L",
                1,
                "PART",
                1,
                null,
                null,
                1,
                1,
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
            this.result.DocType.Should().Be("L");
            this.result.PartNumber.Should().Be("PART");
            this.result.QcState.Should().Be("PASS");
            this.result.QcInfo.Should().Be("Some Info");
            this.result.Lines.Count().Should().Be(1);
            this.result.Lines.First().Id.Should().Be(1111);
        }


        [Test]
        public void ShouldSetPrintLabelsTrue()
        {
            this.result.PrintLabels.Should().BeFalse();
        }
    }
}
