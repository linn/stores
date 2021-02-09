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

    public class WhenCreatingAndUserDoesNotHavePrivilege : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] { AuthorisedAction.UpdateStockLocator };

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
                                        AuditedByDepartmentCode = "CODE",
                                        AuditFrequencyWeeks = 26
                                  }
                          };

        [SetUp]
        public void SetUp()
        {
            this.AuthService
                .HasPermissionFor(AuthorisedAction.CreateStockLocator, Arg.Any<IEnumerable<string>>())
                .Returns(false);
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(this.pallets.AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<StockLocatorException>(()
                => this.Sut.CreateStockLocator(this.toCreate, this.auditDepartmentCode, this.privileges));
            ex.Message.Should().Be("You are not authorised to create.");
        }
    }
}
