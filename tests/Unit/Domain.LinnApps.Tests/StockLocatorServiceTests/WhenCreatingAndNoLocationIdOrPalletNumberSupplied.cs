﻿namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
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

    public class WhenCreatingAndNoLocationIdOrPalletNumberSupplied : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] { AuthorisedAction.CreateStockLocator };

        private readonly StockLocator toCreate = new StockLocator
                                                     {
                                                         Id = 1
                                                     };

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

        [SetUp]
        public void SetUp()
        {
            this.AuthService
                .HasPermissionFor(AuthorisedAction.CreateStockLocator, Arg.Any<IEnumerable<string>>())
                .Returns(true);
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(this.pallets.AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<StockLocatorException>(()
                => this.Sut
                    .CreateStockLocator(this.toCreate, this.auditDepartmentCode, this.privileges));
            ex.Message.Should().Be("Must Supply EITHER Location Id OR Pallet Number");
        }

        [Test]
        public void ShouldNotUpdatePallets()
        {
            Assert.Throws<StockLocatorException>(()
                => this.Sut
                    .CreateStockLocator(this.toCreate, this.auditDepartmentCode, this.privileges));
            this.pallets.All(p => p.AuditFrequencyWeeks != 26).Should().Be(true);
        }
    }
}
