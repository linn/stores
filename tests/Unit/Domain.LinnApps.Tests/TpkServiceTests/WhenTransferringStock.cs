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
            var toTransfer = new List<TransferableStock> { new TransferableStock { FromLocation = "A" } };
                                     this.AccountingCompaniesRepository
                                         .FindBy(Arg.Any<Expression<Func<AccountingCompany, bool>>>()).Returns(
                new AccountingCompany { Name = "LINN", LatesSalesAllocationDate = DateTime.UnixEpoch });
            this.TpkView.FilterBy(Arg.Any<Expression<Func<TransferableStock, bool>>>())
                .Returns(this.repositoryResult.AsQueryable());

            this.whatToWandService.WhatToWand("A")
                .Returns(new List<WhatToWandLine> { new WhatToWandLine { ConsignmentId = 1 } });

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
            this.whatToWandService.Received().WhatToWand("A");
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Should().BeOfType<TpkResult>();
            this.result.WhatToWand.First().ConsignmentId.Should().Be(1);
        }
    }
}
