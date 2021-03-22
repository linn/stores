namespace Linn.Stores.Facade.Tests.StoragePlaceServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStoragePlaceAndBothPalletNumberAndLocationIdSupplied 
        : ContextBase
    {
        private readonly StoragePlaceRequestResource requestResource =
            new StoragePlaceRequestResource { LocationId = 100, PalletNumber = 100 };

        private IResult<StoragePlace> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetStoragePlace(this.requestResource);
        }

        [Test]
        public void ShouldNotCallRepository()
        {
            this.StoragePlaceRepository
                .DidNotReceive().FindBy(Arg.Any<Expression<Func<StoragePlace, bool>>>());
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<StoragePlace>>();
            ((BadRequestResult<StoragePlace>)this.result)
                .Message.Should().Be("Must supply EITHER Location Id or Pallet Number");
        }
    }
}
