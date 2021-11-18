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

    public class WhenBookingInRsnAndPrintRsnLabelsTrue : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PurchaseOrderPack.GetDocumentType(1).Returns("PO");
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.GoodsInPack.GetNextBookInRef().ReturnsForAnyArgs(1);
            this.GoodsInPack.GetRsnDetails(1, out _, out _, out _, out _, out _, out _).Returns(x =>
                {
                    x[4] = 1;
                    x[5] = 123456;
                    return true;
                });

            this.ReqRepository.FindById(1).Returns(new RequisitionHeader { ReqNumber = 1, });
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new Part
                {
                    PartNumber = "PART",
                    Description = "A PART",
                    QcInformation = "Some Info"
                });

            this.GoodsInPack.When(x => x.DoBookIn(
                1,
                "R",
                1,
                "PART",
                1,
                null,
                null,
                null,
                null,
                1,
                "P1234",
                null,
                null,
                null,
                null,
                null,
                null,
                out _,
                out _))
                .Do(x =>
                {
                    x[17] = 1;
                    x[18] = true;
                });

            this.GoodsInPack.GetNextLogId().Returns(1111);

            this.LabelTypeRepository.FindBy(Arg.Any<Expression<Func<StoresLabelType, bool>>>())
                .Returns(new StoresLabelType { DefaultPrinter = "PRINTER", FileName = "rsn_lab" }); 

            this.result = this.Sut.DoBookIn(
                "R",
                1,
                "PART",
                null,
                1,
                null,
                null,
                null,
                null,
                1,
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
                true,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                LoanLine = 1,
                                LoanNumber = 1,
                                StoragePlace = "P1234",
                                Quantity = 1
                            }
                    });
        }

        [Test]
        public void ShouldCallStoredProcedure()
        {
            this.GoodsInPack.Received().DoBookIn(
                1,
                "R",
                1,
                "PART",
                1,
                null,
                null,
                null,
                null,
                1,
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
            this.result.Message.Should().Be("Book In Successful!");
        }

        [Test]
        public void ShouldPrintRsnLabels()
        {
            this.Bartender.Received().PrintLabels("RSN 1", "PRINTER", 1, "rsn_lab", "\"1\",\"PART\",\"123456\"", ref Arg.Any<string>());
        }

        [Test]
        public void ShouldSetPrintLabelsFalse()
        {
            this.result.PrintLabels.Should().BeFalse();
        }
    }
}
