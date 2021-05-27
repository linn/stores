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

    public class WhenUpdatingAndUserDoesNotHavePrivilege : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] { "irrelevant.privilege" };

        private readonly StockLocator to = new StockLocator { Id = 1, LocationId = 1, BatchRef = "Updated Ref" };

        private readonly StockLocator from = new StockLocator { Id = 1, LocationId = 0 };

        [SetUp]
        public void SetUp()
        {
            this.AuthService.HasPermissionFor(AuthorisedAction.UpdateStockLocator, Arg.Any<IEnumerable<string>>())
                .Returns(false);
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(Enumerable.Empty<StoresPallet>().AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<StockLocatorException>(
                () => this.Sut.UpdateStockLocator(this.from, this.to, this.privileges));
            ex.Message.Should().Be("You are not authorised to update.");
        }
    }
}
