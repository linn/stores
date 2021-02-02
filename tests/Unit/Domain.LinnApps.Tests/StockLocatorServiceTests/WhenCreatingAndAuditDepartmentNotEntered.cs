namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndAuditDepartmentNotEntered : ContextBase
    {
        private readonly StockLocator toCreate = new StockLocator
                                                     {
                                                         Id = 1,
                                                         PalletNumber = 1
                                                     };

        private readonly string auditDepartmentCode = null;

        private readonly IEnumerable<StoresPallet> 
            pallets = new List<StoresPallet>
                          {
                              new StoresPallet
                                  {
                                        PalletNumber = 1,
                                        AuditedByDepartmentCode = null,
                                        AuditFrequencyWeeks = null
                                  }
                          };

        [SetUp]
        public void SetUp()
        {
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(this.pallets.AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<StockLocatorException>(()
                => this.Sut.CreateStockLocator(this.toCreate, this.auditDepartmentCode));
            ex.Message.Should().Be("Audit department must be entered");
        }

        [Test]
        public void ShouldNotUpdatePallets()
        {
             Assert.Throws<StockLocatorException>(()
                => this.Sut.CreateStockLocator(this.toCreate, this.auditDepartmentCode));
            this.pallets.All(p => p.AuditFrequencyWeeks != 26).Should().Be(true);
        }
    }
}
