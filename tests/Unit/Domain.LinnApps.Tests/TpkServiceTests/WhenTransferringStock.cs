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
            new List<TransferableStock>
                {
                    new TransferableStock
                        {
                            FromLocation = "A", LocationId = 1, PalletNumber = 1, ConsignmentId = 1
                        },
                    new TransferableStock
                        {
                            FromLocation = "A", LocationId = 1, PalletNumber = 1, ConsignmentId = 2
                        },
                    new TransferableStock
                        {
                            FromLocation = "A", LocationId = 1, PalletNumber = 1, ConsignmentId = 2
                        },
                    new TransferableStock
                        {
                            FromLocation = "A", LocationId = 1, PalletNumber = 1, ConsignmentId = 3
                        }
                };

        private TpkResult result; 

        [SetUp]
        public void SetUp()
        {
            var toTransfer = this.repositoryResult.ToList();
                                     
            this.AccountingCompaniesRepository
                                         .FindBy(Arg.Any<Expression<Func<AccountingCompany, bool>>>()).Returns(
                new AccountingCompany { Name = "LINN", LatesSalesAllocationDate = DateTime.UnixEpoch });
            
            this.TpkView.FilterBy(Arg.Any<Expression<Func<TransferableStock, bool>>>())
                .Returns(this.repositoryResult.AsQueryable());

            this.WhatToWandService.WhatToWand(Arg.Any<int?>(), Arg.Any<int?>())
                .Returns(new List<WhatToWandLine>
                             {
                                 new WhatToWandLine { OrderNumber = 1, ConsignmentId = 1 },
                                 new WhatToWandLine { OrderNumber = 2, ConsignmentId = 2 },
                                 new WhatToWandLine { OrderNumber = 3, ConsignmentId = 2 },
                                 new WhatToWandLine { OrderNumber = 4, ConsignmentId = 3 },
                             });

            this.WhatToWandService.ShouldPrintWhatToWand(toTransfer.First().FromLocation).Returns(true);

            this.TpkPack.When(x => x.UpdateQuantityPrinted(Arg.Any<string>(), out _))
                .Do(x =>
                    {
                        x[1] = true;
                    });

            this.StoresPack.When(x => x.DoTpk(1, 1, Arg.Any<DateTime>(), out _))
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
            this.WhatToWandService.Received().WhatToWand(Arg.Any<int?>(), Arg.Any<int?>());
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
            this.result.Consignments.First().Consignment.ConsignmentId.Should().Be(1);
        }

        [Test]
        public void ShouldGroupConsignments()
        {
            this.result.Consignments.Count().Should().Be(3);
            this.result.Consignments.Single(c => c.Consignment.ConsignmentId == 1).Lines.Count().Should().Be(1);
            this.result.Consignments.Single(c => c.Consignment.ConsignmentId == 2).Lines.Count().Should().Be(2);
            this.result.Consignments.Single(c => c.Consignment.ConsignmentId == 3).Lines.Count().Should().Be(1);
        }
    }
}
