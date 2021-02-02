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

    public class WhenCreating : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] { AuthorisedAction.CreateStockLocator };

        private readonly StockLocator toCreate = new StockLocator
                                                     {
                                                         Id = 1,
                                                         LocationId = 1
                                                     };

        private StockLocator result;

        [SetUp]
        public void SetUp()
        {
            this.AuthService
                .HasPermissionFor(AuthorisedAction.CreateStockLocator, Arg.Any<IEnumerable<string>>())
                .Returns(true);
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(Enumerable.Empty<StoresPallet>().AsQueryable());
            this.result = this.Sut.CreateStockLocator(this.toCreate, null, this.privileges);
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Id.Should().Be(1);
        }

        [Test]
        public void ShouldSetStockPoolCode()
        {
            this.result.StockPoolCode.Should().Be("LINN DEPT");
        }

        [Test]
        public void ShouldSetState()
        {
            this.result.State.Should().Be("STORES");
        }

        [Test]
        public void ShouldSetCategory()
        {
            this.result.Category.Should().Be("FREE");
        }

        [Test]
        public void ShouldSetQuantity()
        {
            this.result.Quantity.Should().Be(1);
        }
    }
}
