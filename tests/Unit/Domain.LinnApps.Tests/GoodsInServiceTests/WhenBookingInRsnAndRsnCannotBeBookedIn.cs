namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInRsnAndRsnCannotBeBookedIn : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PurchaseOrderPack.GetDocumentType(1).Returns("PO");
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.GoodsInPack.GetRsnDetails(1, out _, out _, out _, out _, out _, out _).Returns(x =>
            {
                x[6] = "Cannot book in rsn";
                return false;
            });

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
                false,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                LoanLine = 1,
                                LoanNumber = 1,
                                StoragePlace = "P1234"
                            }
                    });
        }

        [Test]
        public void ShouldNotCallStoredProcedure()
        {
            this.GoodsInPack.DidNotReceiveWithAnyArgs().DoBookIn(
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
        public void ShouldReturnSuccessResult()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("Cannot book in rsn");
        }

        [Test]
        public void ShouldNotPrintRsnLabels()
        {
            this.Bartender.DidNotReceiveWithAnyArgs().PrintLabels(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>(), ref Arg.Any<string>());
        }
    }
}
