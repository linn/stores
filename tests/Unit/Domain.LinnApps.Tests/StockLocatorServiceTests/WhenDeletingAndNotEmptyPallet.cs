namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingAndNotEmptyPallet : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] { AuthorisedAction.UpdateStockLocator };

        private readonly StockLocator toDelete = new StockLocator
                                                     {
                                                         Id = 1,
                                                         PalletNumber = 1
                                                     };

        private readonly List<StockLocator> otherThingsInPallet = 
            new List<StockLocator>
                {
                    new StockLocator
                        {
                            Id = 2,
                            PalletNumber = 1
                        },
                    new StockLocator
                        {
                            Id = 3,
                            PalletNumber = 1
                        }
                };

        [SetUp]
        public void SetUp()
        {
            this.AuthService
                .HasPermissionFor(AuthorisedAction.UpdateStockLocator, Arg.Any<IEnumerable<string>>())
                .Returns(true);
            this.StockLocatorRepository.FilterBy(Arg.Any<Expression<Func<StockLocator, bool>>>())
                .Returns(this.otherThingsInPallet.AsQueryable());
            this.Sut.DeleteStockLocator(this.toDelete, this.privileges);
        }

        [Test]
        public void ShouldDeleteStockLocator()
        {
            this.StockLocatorRepository.Received().Remove(Arg.Any<StockLocator>());
        }

        [Test]
        public void ShouldNotUpdatePallets()
        {
            this.StoresPalletRepository.DidNotReceive()
                .FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>());
        }
    }
}
