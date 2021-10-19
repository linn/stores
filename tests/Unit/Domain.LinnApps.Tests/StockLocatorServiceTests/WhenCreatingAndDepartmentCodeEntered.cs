namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndAuditDepartmentEntered : ContextBase
    {
        private readonly StockLocator toCreate = new StockLocator
        {
            Id = 1,
            PalletNumber = 1
        };

        private readonly IEnumerable<string> privileges = new[] { AuthorisedAction.CreateStockLocator };

        private readonly string auditDepartmentCode = "1234";

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

        private StockLocator result;

        [SetUp]
        public void SetUp()
        {
            this.AuthService
                .HasPermissionFor(AuthorisedAction.CreateStockLocator, Arg.Any<IEnumerable<string>>())
                .Returns(true);
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(this.pallets.AsQueryable());
            
            this.result = this.Sut
                .CreateStockLocator(this.toCreate, this.auditDepartmentCode, this.privileges);
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Id.Should().Be(1);
        }

        [Test]
        public void ShouldUpdatePallets()
        {
            this.pallets.All(p => p.AuditedByDepartmentCode.Equals("1234") 
            && p.AuditFrequencyWeeks == 26).Should().BeTrue();
        }
    }
}
