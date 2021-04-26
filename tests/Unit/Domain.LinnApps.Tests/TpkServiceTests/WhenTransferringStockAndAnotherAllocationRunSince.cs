namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
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

    public class WhenTransferringStockAndAnotherAllocationRunSince : ContextBase
    {
        private readonly IEnumerable<TransferableStock> repositoryResult 
            = new List<TransferableStock>
                  {
                      new TransferableStock { FromLocation = "A" },
                  };

        [SetUp]
        public void SetUp()
        {
            this.AccountingCompaniesRepository.FindBy(Arg.Any<Expression<Func<AccountingCompany, bool>>>()).Returns(
                new AccountingCompany { Name = "LINN", LatesSalesAllocationDate = DateTime.UnixEpoch.AddDays(1) });
            this.TpkView
                .FilterBy(Arg.Any<Expression<Func<TransferableStock, bool>>>())
                .Returns(this.repositoryResult.AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var toTransfer = new List<TransferableStock>
                                 {
                                     new TransferableStock { FromLocation = "A" }
                                 };
            var ex = Assert.Throws<TpkException>(() => 
                this.Sut.TransferStock(new TpkRequest
                                           {
                                               StockToTransfer = toTransfer,
                                               DateTimeTpkViewQueried = DateTime.UnixEpoch
                                           }));
            ex.Message.Should()
                .Be("Another allocation was run at " + DateTime.UnixEpoch.AddDays(1) +
            ". You must re-query the TPK screen to get an up to date version.");
        }
    }
}
