﻿namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMultipleBookInAndFirstLineNotPalletAndSecondLinePallet 
        : ContextBase
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
                });

            this.GoodsInPack.When(x => x.GetPurchaseOrderDetails(
                1000,
                1,
                out var _,
                out var _,
                out var uom,
                out var _,
                out var _,
                out var _,
                out var _,
                out var _))
                .Do(x =>
                {
                    x[4] = "ONES";
                });

            this.GoodsInPack.When(x => x.DoBookIn(
                1,
                "O",
                1,
                "PART",
                1,
                1000,
                1,
                null,
                null,
                null,
                "E-K1-SOMETHING",
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

            this.PurchaseOrderPack.GetDocumentType(1000).Returns("PO");

            this.GoodsInPack.ParcelRequired(1000, null, null, out _)
                .Returns(x =>
                {
                    x[3] = 12345;
                    return true;
                });

            this.result = this.Sut.DoBookIn(
                "O",
                1,
                "PART",
                null,
                1,
                1000,
                1,
                null,
                null,
                null,
                null,
                null,
                "E-K1-SOMETHING",
                null,
                null,
                null,
                null,
                null,
                1,
                true,
                false,
                new List<GoodsInLogEntry>
                    {
                        new GoodsInLogEntry
                            {
                                ArticleNumber = "PART",
                                DateCreated = DateTime.UnixEpoch,
                                StoragePlace = "P1234",
                                OrderLine = 1,
                                OrderNumber = 1000
                            }
                    });
        }

        [Test]
        public void ShouldOnlyCallPalletAnalysisPackCheckOnPalletLine()
        {
            this.PalletAnalysisPack.Received(1).CanPutPartOnPallet(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
