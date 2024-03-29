﻿namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenTransferringStockAndQuantityUpdateFails : ContextBase
    {
        private readonly IEnumerable<TransferableStock> repositoryResult =
           new List<TransferableStock> { new TransferableStock { FromLocation = "A" }, };

        private readonly List<TransferableStock> toTransfer = new List<TransferableStock>
                                                 {
                                                     new TransferableStock
                                                         {
                                                             FromLocation = "A"
                                                         }
                                                 };

        [SetUp]
        public void SetUp()
        {
            this.AccountingCompaniesRepository
                                         .FindBy(Arg.Any<Expression<Func<AccountingCompany, bool>>>()).Returns(
                new AccountingCompany { Name = "LINN", LatesSalesAllocationDate = DateTime.UnixEpoch });

            this.TpkView.FilterBy(Arg.Any<Expression<Func<TransferableStock, bool>>>())
                .Returns(this.repositoryResult.AsQueryable());

            this.WhatToWandService.WhatToWand(Arg.Any<int?>(), Arg.Any<int?>())
                .Returns(new List<WhatToWandLine> { new WhatToWandLine { OrderNumber = 1 } });

            this.TpkPack.When(x => x.UpdateQuantityPrinted(Arg.Any<string>(), out var success))
                .Do(x =>
                {
                    x[1] = false;
                });
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<TpkException>(() => 
                this.Sut.TransferStock(new TpkRequest
                                           {
                                               StockToTransfer = this.toTransfer,
                                               DateTimeTpkViewQueried = DateTime.UnixEpoch
                                           }));
            ex.Message.Should()
                .Be("Failed in update_qty_printed.");
        }
    }
}
