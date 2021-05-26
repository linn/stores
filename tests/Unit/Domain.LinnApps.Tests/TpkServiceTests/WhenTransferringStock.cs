namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenTransferringStock : ContextBase
    {
        private readonly IEnumerable<TransferableStock> repositoryResult =
            new List<TransferableStock> { new TransferableStock { FromLocation = "A" }, };

        private TpkResult result; 

        [SetUp]
        public void SetUp()
        {
            var toTransfer = new List<TransferableStock>
                                 {
                                     new TransferableStock
                                         {
                                             FromLocation = "A", LocationId = 1, PalletNumber = 1, ConsignmentId = 1
                                         }
                                 };
                                     
            this.AccountingCompaniesRepository
                                         .FindBy(Arg.Any<Expression<Func<AccountingCompany, bool>>>()).Returns(
                new AccountingCompany { Name = "LINN", LatesSalesAllocationDate = DateTime.UnixEpoch });
            
            this.TpkView.FilterBy(Arg.Any<Expression<Func<TransferableStock, bool>>>())
                .Returns(this.repositoryResult.AsQueryable());

            this.WhatToWandService.WhatToWand(Arg.Any<int>())
                .Returns(new List<WhatToWandLine> { new WhatToWandLine { OrderNumber = 1 } });

            this.TpkPack.When(x => x.UpdateQuantityPrinted(Arg.Any<string>(), out var success))
                .Do(x =>
                    {
                        x[1] = true;
                    });

            this.StoresPack.When(x => x.DoTpk(1, 1, Arg.Any<DateTime>(), out var success))
                .Do(x =>
                    {
                        x[3] = true;
                    });
            
            this.result = this.Sut.TransferStock(new TpkRequest
                                                     {
                                                         StockToTransfer = toTransfer,
                                                         DateTimeTpkViewQueried = DateTime.Now
                                                     });
        }

        [Test]
        public void ShouldPrintBundleLabels()
        {
            this.BundleLabelPack.Received().PrintTpkBoxLabels("A");
        }

        [Test]
        public void ShouldGetWhatToWandData()
        {
            this.WhatToWandService.Received().WhatToWand(Arg.Any<int>());
        }

        [Test]
        public void ShouldCallUpdateQuantity()
        {
            this.TpkPack.Received().UpdateQuantityPrinted(Arg.Any<string>(), out Arg.Any<bool>());
        }

        [Test]
        public void ShouldDoTpk()
        {
            this.StoresPack.Received().DoTpk(1, 1, Arg.Any<DateTime>(), out Arg.Any<bool>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Should().BeOfType<TpkResult>();
            this.result.Report.Consignment.ConsignmentId.Should().Be(1);
        }
    }
}
