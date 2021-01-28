namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingAndEmptyPallet : ContextBase
    {
        private readonly StockLocator toDelete = new StockLocator
                                                     {
                                                         Id = 1,
                                                         PalletNumber = 1
                                                     };

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorRepository.FilterBy(Arg.Any<Expression<Func<StockLocator, bool>>>())
                .Returns(Enumerable.Empty<StockLocator>().AsQueryable());
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(new List<StoresPallet> 
                             { 
                                 new StoresPallet
                                     {
                                         PalletNumber = 1,
                                     }
                             }.AsQueryable());
            this.Sut.DeleteStockLocator(this.toDelete);
        }

        [Test]
        public void ShouldDeleteStockLocator()
        {
            this.StockLocatorRepository.Received().Remove(Arg.Any<StockLocator>());
        }

        [Test]
        public void ShouldUpdatePallets()
        {
            this.StoresPalletRepository.Received()
                .UpdatePallet(1, null, null);
        }
    }
}
